using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using log4net.Appender;
using log4net.spi;
using System.Web.Configuration;

namespace propeller.logger.Appenders
{
    public class UdpTextAppender : AppenderSkeleton
    {

        private Object localLock = new Object();
        private UdpClient _udpClient;
        private int portNumber;
        private IPAddress _ipAddress;
        private bool _isValidated = false;


        public string Port { get; set; }

        public string Destination { get; set; }
       
        public UdpTextAppender()
        {
            
            _udpClient = new UdpClient();
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            // Get url path
            try
            {
                var path = HttpContext.Current.Request.Url.AbsoluteUri;
                loggingEvent.Properties["uri"] = path;
            }
            catch (Exception e)
            {
                loggingEvent.Properties["uri"] = "N/A";                
            }
            
            
            
            
            
            StackTrace stackTrace = new StackTrace();

            // Get calling method name
            string callersFulleName = string.IsNullOrWhiteSpace(loggingEvent.LoggerName) ? "Unable_to_resolve_caller" : loggingEvent.LoggerName;
            if (callersFulleName.Contains('['))
                callersFulleName = callersFulleName.Split('[').FirstOrDefault();
            

            var machineName = System.Environment.MachineName;
            loggingEvent.Properties["hostname"] = string.IsNullOrWhiteSpace(machineName) ? "MachineName_not_available" : machineName;
            try
            {
                var siteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
                loggingEvent.Properties["sitename"] = string.IsNullOrWhiteSpace(siteName) ? "not_available" : siteName;

            }
            catch (Exception e)
            {
                loggingEvent.Properties["sitename"] = "MachineName_not_available";

            }

            loggingEvent.Properties["log_owner"] = callersFulleName;

            var configuredSitename = WebConfigurationManager.AppSettings["elkIndexName"];
            loggingEvent.Properties["indexname"] = string.IsNullOrWhiteSpace(configuredSitename) ? "sitecore_index" : configuredSitename;
            


            if (!_isValidated)
                ValidateConfiguration();

            var ipEndPoint = new IPEndPoint(_ipAddress, portNumber);
            var logMessage = RenderLoggingEvent(loggingEvent);
            var message = Encoding.ASCII.GetBytes(logMessage);
            lock (localLock)
            {
                _udpClient.Send(message, message.Length, ipEndPoint);
            }
            
        }

        private void ValidateConfiguration()
        {
            // Validate port
            if (Port == null || !int.TryParse(Port, out portNumber))
                throw new ArgumentException("Invalid port number for Udp appender");

            // validate desination
            var hostAddresses = Dns.GetHostAddresses(Destination);
            if (hostAddresses == null || hostAddresses.Length < 1)
                throw new ArgumentException("Invaild destination address for udp appender");

            _ipAddress = hostAddresses.First(p => p.AddressFamily == AddressFamily.InterNetwork);
            _isValidated = true;
        }

        public override void OnClose()
        {
            _udpClient.Close();
        }

       
    }
}
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using log4net.Appender;
using log4net.spi;

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

            loggingEvent.Properties["hostname"] = System.Environment.MachineName;
            try
            {
                loggingEvent.Properties["sitename"] = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
            }
            catch (Exception e)
            {
                loggingEvent.Properties["sitename"] = "N/A";

            }

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
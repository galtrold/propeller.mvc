using System;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Propeller.Log
{
    public class SitecoreTcpAppender : AppenderSkeleton
    {
        private static volatile RemotingAppender instance;
        private static object syncRoot = new Object();
        public virtual ILayout Layout { get; set; }

        public RollingFileAppender.RollingMode RollingStyle { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var appender = Instance(this);
            appender.DoAppend(loggingEvent);
        }

        public static RemotingAppender Instance(SitecoreTcpAppender paramsters)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {


                        instance = new RemotingAppender()
                        {
                            Sink = "tcp://localhost:8085",
                            Lossy = false,
                            BufferSize = 1

                        };
                        //{
                        //    File = paramsters._absoluteFilepath,
                        //    AppendToFile = paramsters.AppendToFile,
                        //    RollingStyle = paramsters.RollingStyle,
                        //    MaxSizeRollBackups = paramsters.MaxSizeRollBackups,
                        //    MaximumFileSize = paramsters.MaximumFileSize,
                        //    StaticLogFileName = paramsters.StaticLogFileName
                        //};
                        //var layout = new PatternLayout("%date [%thread] %level %logger - %message%newline");
                        instance.Layout = paramsters.Layout;
                        instance.ActivateOptions();
                    }

                }
            }
            return instance;
        }
        public static void StartClient(string message)
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

using System;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

namespace Propeller.Log
{
    public class SitecoreTcpAppender : AppenderSkeleton
    {
        private static volatile RemotingAppender instance;
        private static object syncRoot = new Object();
        public virtual string File { get; set; }
        public virtual bool AppendToFile { get; set; }
        public virtual int MaxSizeRollBackups { get; set; }

        public virtual string MaximumFileSize { get; set; }

        public virtual bool StaticLogFileName { get; set; }

        public virtual ILayout Layout { get; set; }

        private string _absoluteFilepath = "";

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
                            BufferSize = 20,
                            Fix = FixFlags.All

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
    }
}

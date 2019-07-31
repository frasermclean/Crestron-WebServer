using System;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.Diagnostics;
using Crestron.SimplSharpPro.DeviceSupport;
using FM.WebServer;

namespace TestSystem
{
    public class ControlSystem : CrestronControlSystem
    {
        #region System components
        WebServer webServer;
        #endregion

        #region Properties
        public bool TraceEnabled { get; set; }
        public string TraceName { get; set; }
        #endregion

        #region Constants
        const int Port = 7000;
        #endregion

        #region Initialization
        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;
            }
            catch (Exception ex)
            {
                string message = String.Format("ControlSystem() exception caught: {0}", ex.Message);
                Trace(message);
                ErrorLog.Exception(message, ex);
            }
        }
        public override void InitializeSystem()
        {
            try
            {
                // trace defaults
                this.TraceEnabled = true;
                this.TraceName = this.GetType().Name;

                // web server component
                webServer = new WebServer(Port);
                webServer.TraceEnabled = true;
                webServer.RequestCallback += new WebServerRequestDelegate(WebServerRequestCallback);
                if (webServer.StartListening())
                    Trace("InitializeSystem() web server started successfully.");
                else
                    Trace("InitializeSystem() web server failed to start.");
            }
            catch (Exception ex)
            {
                string message = String.Format("InitializeSystem() exception caught: {0}", ex.Message);
                Trace(message);
                ErrorLog.Exception(message, ex);
            }
        }
        #endregion

        #region Debugging
        private void Trace(string message)
        {
            if (TraceEnabled)
                CrestronConsole.PrintLine(String.Format("[{0}] {1}", TraceName, message.Trim()));
        }
        #endregion

        #region Event callbacks
        void WebServerRequestCallback(string path)
        {
            Trace("WebServerRequestCallback() received request for path: " + path);
        }
        #endregion
    }
}

﻿using System;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;

namespace FM.WebServer
{
    public class WebServer
    {
        #region Class variables
        HttpServer server;
        int port;
        #endregion

        #region Properties
        public bool TraceEnabled { get; set; }
        public string TraceName { get; set; }
        #endregion

        #region Constants
        const string ServerName = "FM SimplSharp WebServer";
        #endregion

        #region Constructor
        public WebServer(int port)
        {
            this.TraceName = this.GetType().Name;
            this.port = port;
        }
        #endregion

        #region Public methods
        public bool StartListening()
        {
            try
            {
                if (server == null)
                {
                    server = new HttpServer();
                    server.Port = port;
                    server.ServerName = ServerName;
                    server.OnHttpRequest += new OnHttpRequestHandler(ServerHttpRequestHandler);
                    server.Open();

                    return true;
                }
                else
                {
                    Trace("StartListening() server object already exists. No action taken.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Trace("StartListening() exeception caught: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Private methods
        void Trace(string message)
        {
            if (TraceEnabled)
                CrestronConsole.PrintLine(String.Format("[{0}] {1}", TraceName, message.Trim()));
        }
        #endregion

        #region Event callbacks
        void ServerHttpRequestHandler(object sender, OnHttpRequestArgs e)
        {
            Trace("ServerHttpRequestHandler() received request. Path: " + e.Request.Path);
        }
        #endregion
    }
}

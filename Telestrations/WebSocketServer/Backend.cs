using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;
using SuperSocket.SocketEngine.Configuration;
using SuperWebSocket;

namespace WebSocketServer
{
    class Backend
    {
        private List<WebSocketSession> m_Sessions = new List<WebSocketSession>();
        private List<WebSocketSession> m_SecureSessions = new List<WebSocketSession>();
        private object m_SessionSyncRoot = new object();
        private object m_SecureSessionSyncRoot = new object();
        private Timer m_SecureSocketPushTimer;
        private int m_Index = 0;
        private IBootstrap m_Bootstrap;

        public void Application_Start()
        {
            StartSuperWebSocketByProgramming();
            var ts = new TimeSpan(0, 0, 0, 0, 5000);
            m_SecureSocketPushTimer = new Timer(OnSecureSocketPushTimerCallback, new object(), ts, ts);
        }

        void OnSecureSocketPushTimerCallback(object state)
        {
            
            lock (m_SecureSessionSyncRoot)
            {
                m_SecureSessions.ForEach(s => s.Send("Push data from WebSocket. [" + (m_Index++) + "] Current Time: " + DateTime.Now));
            }
        }

        void socketServer_NewMessageReceived(WebSocketSession session, string e)
        {
            Console.WriteLine("Received");
            SendToAll(session.Cookies["name"] + ": " + e);
        }

        void secureSocketServer_SessionClosed(WebSocketSession session, CloseReason reason)
        {
            lock (m_SecureSessionSyncRoot)
            {
                m_SecureSessions.Remove(session);
            }
        }

        void secureSocketServer_NewSessionConnected(WebSocketSession session)
        {
            lock (m_SecureSessionSyncRoot)
            {
                m_SecureSessions.Add(session);
            }
        }

        void StartSuperWebSocketByProgramming()
        {
            var rootConfig = new RootConfig();

            var socketServer = new SuperWebSocket.WebSocketServer();

            socketServer.NewMessageReceived += socketServer_NewMessageReceived;
            socketServer.NewSessionConnected += socketServer_NewSessionConnected;
            socketServer.SessionClosed += socketServer_SessionClosed;

            socketServer.Setup(rootConfig,
                new ServerConfig
                {
                    Name = "SuperWebSocket",
                    Ip = "Any",
                    Port = 2011,
                    Mode = SocketMode.Tcp
                });

            var secureSocketServer = new SuperWebSocket.WebSocketServer();
            secureSocketServer.NewSessionConnected += secureSocketServer_NewSessionConnected;
            secureSocketServer.SessionClosed += secureSocketServer_SessionClosed;

            secureSocketServer.Setup(rootConfig, new ServerConfig
            {
                Name = "SecureSuperWebSocket",
                Ip = "Any",
                Port = 2012,
                Mode = SocketMode.Tcp,
                //Security = "tls",
                //Certificate = new SuperSocket.SocketBase.Config.CertificateConfig
                //{
                //    FilePath = Server.MapPath("~/localhost.pfx"),
                //    Password = "supersocket"
                //}
            });

            m_Bootstrap = new DefaultBootstrap(new RootConfig(), new IWorkItem[] { socketServer, secureSocketServer });

            //MediaTypeNames.Application["WebSocketPort"] = socketServer.Config.Port;
            //MediaTypeNames.Application["SecureWebSocketPort"] = secureSocketServer.Config.Port;

            m_Bootstrap.Start();

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }
        }

        void socketServer_NewSessionConnected(WebSocketSession session)
        {
            lock (m_SessionSyncRoot)
                m_Sessions.Add(session);

            SendToAll("System: " + session.Cookies["name"] + " connected");
        }

        void socketServer_SessionClosed(WebSocketSession session, CloseReason reason)
        {
            lock (m_SessionSyncRoot)
                m_Sessions.Remove(session);

            if (reason == CloseReason.ServerShutdown)
                return;

            SendToAll("System: " + session.Cookies["name"] + " disconnected");
        }

        void SendToAll(string message)
        {
            lock (m_SessionSyncRoot)
            {
                foreach (var s in m_Sessions)
                {
                    s.Send(message);
                }
            }
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Alchemy;
using Alchemy.Classes;
using Zargess.VHKPlayer.FileManagement;

namespace Zargess.VHKPlayer.WebSocket {
    public class WebServer {
        public delegate void MessageHandler(object sender, MessageEventArgs e);

        public event MessageHandler MessageSent;

        public int Port { get; private set; }
        protected static ConcurrentDictionary<string, Connection> OnlineConnections = new ConcurrentDictionary<string, Connection>();
        private TcpServer Server { get; set; }

        public WebServer(int port) {
            Port = port;
        }

        public void StartServer() {
            Server = new WebSocketServer(Port, IPAddress.Any) {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnected = OnConnect,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };
            Server.Start();
            PrintToConsole("Running Alchemy WebSocket Server ...");
        }

        public void CheckCommands(string command, string[] args) {
            switch (command) {
                case "exit":
                    Shutdown();
                    break;
                case "send":
                    var s = "";
                    args.ToList().ForEach(x => s += x);
                    SendToAll("msg", s);
                    break;
                case "sendXml":
                    //SendToAll("xml", Manager.WriteToString());
                    break;
                case "start":
                    StartServer();
                    break;
                default:
                    Console.WriteLine("'{0}' is not a command", command);
                    break;
            }

        }

        public void Shutdown() {
            SendToAll("disconnect", "");
            Server.Stop();
            Server.Dispose();
        }

        public void OnConnect(UserContext aContext) {
            PrintToConsole("Client Connected From : " + aContext.ClientAddress);

            // Create a new Connection Object to save client context information
            var conn = new Connection { Context = aContext };

            // Add a connection Object to thread-safe collection
            OnlineConnections.TryAdd(aContext.ClientAddress.ToString(), conn);

        }

        public void OnReceive(UserContext aContext) {
            try {
                PrintToConsole("Data Received From [" + aContext.ClientAddress + "] - " + aContext.DataFrame);
                CheckRequest(aContext.DataFrame.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void CheckRequest(string request) {
            switch (request) {
                case "getStructure":
                    //SendToAll("xml", Manager.WriteToString());
                    break;
                default:
                    SendToAll("string", "400");
                    break;
            }
        }

        public void OnSend(UserContext aContext) {
            PrintToConsole("Data: " + aContext.DataFrame + " Sent To : " + aContext.ClientAddress);
        }

        public void OnDisconnect(UserContext aContext) {
            Console.WriteLine("Client Disconnected : " + aContext.ClientAddress);

            // Remove the connection Object from the thread-safe collection
            Connection conn;
            OnlineConnections.TryRemove(aContext.ClientAddress.ToString(), out conn);

            // Dispose timer to stop sending messages to the client.
            if (conn.Timer != null) {
                conn.Timer.Dispose();
            }
        }

        public void SendToAll(string type, string msg) {
            foreach (var key in OnlineConnections.Keys) {
                OnlineConnections[key].SendMessage(type + " " + msg);
            }
        }

        private void PrintToConsole(string text) {
            if (MessageSent != null) {
                MessageSent(this, new MessageEventArgs(text));
            }
        }
    }
}
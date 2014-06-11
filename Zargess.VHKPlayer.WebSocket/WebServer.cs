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
        public int Port { get; private set; }
        public StructureManager Manager { get; set; }
        protected static ConcurrentDictionary<string, Connection> OnlineConnections = new ConcurrentDictionary<string, Connection>();
        
        public WebServer(int port, StructureManager manager) {
            Port = port;
            Manager = manager;
        }

        public void StartServer() {
            var aServer = new WebSocketServer(Port, IPAddress.Any) {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnected = OnConnect,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };
            aServer.Start();


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "Alchemy WebSocket Server";
            Console.WriteLine("Running Alchemy WebSocket Server ...");
            Console.WriteLine("[Type \"exit\" and hit enter to stop the server]");
            Console.WriteLine("[Type \"help\" and hit enter to show list of commands]");

            CheckCommands(aServer);
        }

        private void CheckCommands(WebSocketServer aServer) {
            var running = true;
            while (running) {
                var input = Console.ReadLine().Split(' ');
                if (input.Length == 0) { continue; }
                var command = input[0];
                var parameter = "";
                for (var i = 1; i < input.Length; i++) {
                    parameter = parameter + " " + input[i];
                }
                switch (command) {
                    case "exit":
                        aServer.Stop();
                        aServer.Dispose();
                        running = false;
                        break;
                    case "send":
                        SendToAll("string", parameter);
                        break;
                    case "sendXml":
                        SendToAll("xml", Manager.WriteToString());
                        break;
                    default:
                        Console.WriteLine("'{0}' is not a command", command);
                        break;
                }
            }
        }

        public void OnConnect(UserContext aContext) {
            Console.WriteLine("Client Connected From : " + aContext.ClientAddress);

            // Create a new Connection Object to save client context information
            var conn = new Connection { Context = aContext };

            // Add a connection Object to thread-safe collection
            OnlineConnections.TryAdd(aContext.ClientAddress.ToString(), conn);

        }

        public void OnReceive(UserContext aContext) {
            try {
                Console.WriteLine("Data Received From [" + aContext.ClientAddress + "] - " + aContext.DataFrame);
                CheckRequest(aContext.DataFrame.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void CheckRequest(string request) {
            switch (request) {
                case "getStructure":
                    SendToAll("xml", Manager.WriteToString());
                    break;
                default:
                    SendToAll("string", "400");
                    break;
            }
        }

        public void OnSend(UserContext aContext) {
            Console.WriteLine("Data Sent To : " + aContext.ClientAddress);
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
    }
}
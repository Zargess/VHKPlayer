using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.NotificationManagement;
using Zargess.VHKPlayer.WebSocket;

namespace Zargess.VHKPlayer.ConsoleView {
    class Program {
        private static void Main(string[] args) {
            var manager = new NotificationManager();
            var x = new StructureManager(@"C:\Users\MFH\vhk", manager);
            x.Document.Save(@"c:\users\mfh\desktop\test.xml");
            Console.ReadLine();
            var server = new WebServer(8100, x);
            server.StartServer();
            ListenForCommands(server);
        }

        private static void ListenForCommands(WebServer server) {
            while (true) {
                var command = Console.ReadLine().Split(' ');
                var cType = command[0];
                var parameter = "";
                for (var i = 1; i < command.Length; i++) {
                    parameter = parameter + " " + command[i];
                }

                switch (cType) {
                    case "server":
                        server.CheckCommands(parameter);
                        break;
                    case "createStructure":
                        var wizard = new StructureWizard();
                        wizard.Begin();
                        break;
                    default:
                        Console.WriteLine("'{0}' is not at command", cType);
                        break;
                }
            }
        }
    }
}

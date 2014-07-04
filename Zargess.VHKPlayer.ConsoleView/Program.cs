using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.WebSocket;

namespace Zargess.VHKPlayer.ConsoleView {
    class Program {
        private static void Main(string[] args) {
            var x = new StructureManager(@"C:\Users\MFH\vhk");
            x.Document.Save(@"c:\users\mfh\desktop\test.xml");
            var server = new WebServer(8100);
            server.StartServer();
            foreach (var p in PlayableCreator.CreatePlayables("files", new[] { @"C:\Users\MFH\Dropbox\Programmering\C#\Damer 2013-2014\Video\Blandet" }).Select(playable => playable as FileNode)) {
                Console.WriteLine(p.Name);
            }
            ListenForCommands(server);
        }

        private static void ListenForCommands(WebServer server) {
            while (true) {
                var command = Console.ReadLine().Split(' ');
                var cType = command[0];
                var parameter = "";
                for (var i = 1; i < command.Length; i++) {
                    if (parameter == "") parameter = command[i];
                    else parameter = parameter + " " + command[i];
                }

                switch (cType) {
                    case "server":
                        server.CheckCommands(parameter);
                        break;
                    default:
                        Console.WriteLine("'{0}' is not at command", cType);
                        break;
                }
            }
        }
    }
}

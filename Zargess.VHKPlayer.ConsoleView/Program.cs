using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.NotificationManagement;

namespace Zargess.VHKPlayer.ConsoleView {
    class Program {
        static void Main(string[] args) {
            var manager = new NotificationManager();
            var x = new XmlManager(@"C:\Users\MFH\vhk", manager);
            x.Document.Save(@"c:\users\mfh\desktop\test.xml");
            var folders = new FolderNode(@"C:\Users\MFH\vhk", false).GetContent();
            Console.WriteLine("");
            Console.ReadLine();
        }
    }
}

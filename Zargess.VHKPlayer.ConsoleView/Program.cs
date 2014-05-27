using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;

namespace Zargess.VHKPlayer.ConsoleView {
    class Program {
        static void Main(string[] args) {
            var x = new XmlManager(@"C:\Users\MFH\vhk");
            x.Document.Save(@"c:\users\mfh\desktop\test.xml");
            var folders = new FolderNode(@"C:\Users\MFH\vhk", false).GetContent();
            Console.WriteLine("");
        }
    }
}

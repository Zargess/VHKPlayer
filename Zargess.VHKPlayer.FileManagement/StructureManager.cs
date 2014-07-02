using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Zargess.VHKPlayer.FileManagement {
    public class StructureManager {
        public FolderNode RootFolder { get; set; }
        public XmlDocument Document { get; set; }
        public readonly FolderNode XmlFolder = new FolderNode(Environment.CurrentDirectory);

        public StructureManager(string path) {
            RootFolder = new FolderNode(path);
            Document = InitDocument();
        }

        public StructureManager(FolderNode root) : this(root.FullPath) { }

        private XmlDocument InitDocument() {
            var doc = new XmlDocument();
            if (XmlFolder.Exists && XmlFolder.ContainsFile("FolderStructure.xml")) {
                doc.Load(XmlFolder.GetFile("FolderStructure.xml").FullPath);
            } else {
                doc = CreateDocument(true);
            }
            doc.Save(PathHandler.CombinePaths(Environment.CurrentDirectory, "FolderStructure.xml"));
            return doc;
        }

        private XmlDocument CreateDocument(bool userRespReq) {
            var doc = new XmlDocument();
            var folders = RootFolder.GetContent();
            var temp = folders.SingleOrDefault(x => x.FullPath == RootFolder.FullPath);
            folders.Remove(temp);

            var root = doc.CreateElement("RootFolder");
            root.SetAttributeNode(MakeAttribute("path", RootFolder.FullPath));
            doc.AppendChild(root);

            foreach (var folder in folders) {
                var element = doc.CreateElement("Folder");
                element.SetAttributeNode(MakeAttribute("path", PathHandler.RemovePathFromNode(folder, RootFolder)));
                root.AppendChild(element);

                var response = "";
                if (userRespReq) {
                    response = UserResponse("Should we show " + folder.FullPath, new[] { "yes", "no" });
                    element.SetAttributeNode(MakeAttribute("marked", (response == "yes").ToString()));
                } else {
                    element.SetAttributeNode(MakeAttribute("marked", userRespReq.ToString()));
                }

                var viewport = "0";
                if (response == "yes") {
                    viewport = UserResponse("Which viewport should we show" + folder.FullPath + 
                        "in? \n 1 or 2 to show it. 0 to hide it.", new[] { "1", "2", "3" });
                }
                element.SetAttributeNode(MakeAttribute("viewport", viewport));

                // Gør sådan at brugeren kan beskrive hvordan en mappe skal loades
            }

            return doc;
        }

        private XmlAttribute MakeAttribute(string name, string value) {
            var doc = new XmlDocument();
            var attribute = doc.CreateAttribute(name);
            attribute.Value = value;
            return attribute;
        }

        private string UserResponse(string question, string[] answers) {
            Console.WriteLine(question);
            var response = "";
            while (true) {
                response = Console.ReadLine();
                if (answers.Any(answer => response == answer)) {
                    break;
                }
            }
            return response;
        }

        public bool DocumentHasFolder(XmlDocument doc, FolderNode fn) {
            return DocumentHasNode(doc, fn.FullPath);
        }

        public bool DocumentHasNode(XmlDocument doc, string path) {
            var node = doc.GetElementsByTagName("RootFolder").Item(0);

            if (node == null) return false;
            foreach (var child in node.ChildNodes) {
                var c = child as XmlNode;
                if (path.StartsWith(RootFolder.FullPath)) {
                    if (c != null && (c.Attributes != null && (RootFolder.FullPath + c.Attributes.Item(0).Value == path))) {
                        return true;
                    }
                } else {
                    if (c != null && (c.Attributes != null && (c.Attributes.Item(0).Value == path))) {
                        return true;
                    }
                }
            }

            return false;
        }

        public string WriteToString() {
            using (var stringwriter = new StringWriter())
            using (var xmlwriter = XmlWriter.Create(stringwriter)) {
                Document.WriteTo(xmlwriter);
                xmlwriter.Flush();
                return stringwriter.GetStringBuilder().ToString();
            }
        }

        public string WriteToStringPretty() {
            var builder = new StringBuilder();
            var settings = new XmlWriterSettings {
                Indent = true,
                IndentChars = "    ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = new UTF8Encoding(false)
            };
            using (var writer = XmlWriter.Create(builder, settings)) {
                Document.Save(writer);
            }
            return builder.ToString();
        }
    }
}
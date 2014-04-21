using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.FileManagement {
    class MemoryManager {
        public bool IsDoneLoading { get; private set; }
        public string Path { get; set; }
        public bool Cancel { get; set; }
        public List<Folder> MemoryList { get; private set; }

        public MemoryManager(string path) {
            MemoryList = new List<Folder>();
            Path = path;
            IsDoneLoading = true;
            StartMemoryLoad();
        }

        public void StartMemoryLoad() {
            var task = new Task(LoadFoldersToMemory);
            task.Start();
        }

        public async void LoadFoldersToMemory() {
            try {
                MemoryList = await LoadAllFolders(Path);
            } catch (UnauthorizedAccessException ue) {
                MessageBox.Show("Du har prøvet at loade en mappe du ikke har rettigheder til at loade ind i programmet.\n" +
                    "Vær venlig at vælge en anden mappe eller ret dine rettigheder til den pågældende mappe og prøv igen.\n" + 
                    ue.Message);
            }
        }

        private async Task<List<Folder>> LoadAllFolders(string path) {
            IsDoneLoading = false;
            var res = new List<Folder>();
            var folders = Directory.GetDirectories(path, "*", SearchOption.AllDirectories).ToList().Select(folder => new Folder(folder)).ToList();

            foreach (var folder in folders.TakeWhile(folder => !Cancel)) {
                Console.WriteLine(folder.FullPath);
                var files = GetFilesInFolder(folder);
                foreach (var file in files) {
                    var f = new File(file.FullPath);
                    if (f.Type != FileType.Unsupported) {
                        folder.AddFile(f);
                    }
                }
                res.Add(folder);
            }
            Cancel = false;
            IsDoneLoading = true;
            return res;
        }

        public List<Folder> GetSubfolders(string path) {
            var temp = PathHandler.SplitPath(path);
            var list = MemoryList.Where(folder => folder.FullPath.Contains(path) && folder.Source == temp[temp.Length - 1]).ToList();
            return list.Select(folder => new Folder(folder.FullPath)).ToList();
        }

        public List<File> RelatedFiles(File fn) {
            var res = new List<File>();
            var folders = MemoryList.Where(x => x.Source == fn.Source);

            foreach (var folder in folders) {
                var files = folder.Content.Where(x => x.NameWithNoExtension == fn.NameWithNoExtension).ToList();
                res.AddRange(files.Select(file => new File(file.FullPath)));
            }

            return res;
        }

        private IEnumerable<File> GetFilesInFolder(Folder folder) {
            var filePaths = Directory.GetDirectories(folder.FullPath, "*", SearchOption.TopDirectoryOnly).ToList();

            return filePaths.Select(filePath => new File(filePath)).ToList();
        }
    }
}

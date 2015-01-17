using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Test.TestClasses {
    public class TestPlayable : IPlayable {
        public ObservableCollection<IFile> Content {
            get {
                throw new NotImplementedException();
            }
        }

        public string Name {
            get {
                throw new NotImplementedException();
            }
        }

        public bool Repeat {
            get {
                throw new NotImplementedException();
            }
        }

        public Queue<IFile> Play(PlayType type) {
            var res = new Queue<IFile>();
            res.Enqueue(new FileNode(@"c:\test\temp.mp3"));
            res.Enqueue(new FileNode(@"c:\test\temp.avi"));
            return res;
        }
    }
}

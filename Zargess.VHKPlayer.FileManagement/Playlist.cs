using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class Playlist : IPlayable {
        public List<FilePair> Content { get; private set; }
        public Dictionary<int, IProcedure> Procedures { get; private set; }
        public bool Repeatable { get; private set; }

        public Playlist(bool repeat) {
            Content = new List<FilePair>();
            Procedures = new Dictionary<int, IProcedure>();
            Repeatable = repeat;

        }
    }

    public class FilePair : IPlayable {
        public FilePair(FileNode video, FileNode audio) {
            Video = video;
            Audio = audio;
        }

        public FileNode Audio { get; set; }
        private FileNode Video { get; set; }
    }
}
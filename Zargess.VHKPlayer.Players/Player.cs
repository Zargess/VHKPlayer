using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;

namespace Zargess.VHKPlayer.Players {
    public class Player {
        public int Number { get; private set; }
        public FileNode StatPicture { get; private set; }
        public FileNode StatMusic { get; private set; }
        public FileNode StatVideo { get; private set; }
        public FileNode Video { get; private set; }
        public FileNode Picture { get; private set; }
        public bool Keeper { get; private set; }
        public string Name { get; private set; }
        public Statistics Stats { get; set; }
        public FolderNode StatFolder { get; set; }

        public Player(string name, int no, bool keeper, FileNode pic, FileNode vid, FileNode statvid, FileNode statmus, 
            FileNode statpic, FolderNode statfold) {
            Name = name;
            Number = no;
            Keeper = keeper;
            Picture = pic;
            Video = vid;
            StatVideo = statvid;
            StatMusic = statmus;
            StatPicture = statpic;
            StatFolder = statfold;
            Stats = new Statistics(StatFolder, Number);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class PlayableFile : IPlayable
    {
        public string Name { get; set; }
        public FileNode File { get; set; }

        public void Play(IPlayStrategy strategy)
        {
            strategy.Play(new List<FileNode>() { File });
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

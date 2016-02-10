using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class PlayableFile : IPlayable
    {
        public string Name { get; set; }
        public FileNode File { get; set; }
        public ICollection<FileNode> Content { get; set; }

        public void Play(IPlayStrategy strategy, IVideoPlayerController videoPlayer)
        {
            strategy.Play(new List<FileNode>() { File }, videoPlayer);
            // TODO : Remove this print
            Console.WriteLine("Playable File: {0}", Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

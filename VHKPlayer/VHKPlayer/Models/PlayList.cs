using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class PlayList : IPlayable, IVhkObserver<FolderNode>
    {
        // TODO : Consider adding a Repeat bool here to indicate if the playlist should repeat it self
        public string Name { get; set; }
        public bool HasAudio { get; set; }
        public ICollection<FileNode> Content { get; set; }
        public ILoadingStrategy<ICollection<FileNode>> LoadingStrategy { get; set; }
        public IPlayStrategy PlayStrategy { get; set; }

        public void Play(IPlayStrategy strategy, IVideoPlayerController videoPlayer)
        {
            PlayStrategy.Play(Content, videoPlayer);
        }

        public void SubjectUpdated(FolderNode subject)
        {
            Content.Clear();
            foreach (var file in LoadingStrategy.Load())
            {
                Content.Add(file);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
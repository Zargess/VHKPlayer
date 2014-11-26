using System;
using System.Linq;
using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.UtilFunctions;
using Zargess.VHKPlayer.SettingsManager;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer {
    public class StatSelectionStrategy : IFileSelectionStrategy {

        public IFileSelectionStrategy VidSelection { get; private set; }
        public IFileSelectionStrategy PicSelection { get; private set; }

        public StatSelectionStrategy(IFileSelectionStrategy picSelection, IFileSelectionStrategy vidSelection) {
            PicSelection = picSelection;
            VidSelection = vidSelection;
        }

        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            var content = playable.GetContent();

            string statFolder = PathHandler.AbsolutePath(SettingsManagement.Instance.GetPathSetting("playerFolders", 2));
            string statMusicFolder = PathHandler.AbsolutePath(SettingsManagement.Instance.GetPathSetting("playerFolders", 3));
            string statVideoFolder = PathHandler.AbsolutePath(SettingsManagement.Instance.GetPathSetting("playerFolders", 4));
            if (content.Count == 1) return PicSelection.SelectFiles(playable);
            if (!content.Any(x => x.FullPath.ToLower().Contains(statFolder))) return VidSelection.SelectFiles(playable);

            var pic = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statFolder));
            var mus = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statMusicFolder));
            var vid = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statVideoFolder));
            if (pic == null || mus == null) throw new FilesMissingException("Player stat files does not meet requirements. There should be atleast one picture and one music file pr. player.");
            res.Enqueue(mus.Clone());
            res.Enqueue(pic.Clone());
            if (vid == null) {
                vid = PicSelection.SelectFiles(playable).Dequeue();
            }
            res.Enqueue(vid.Clone());

            return res;
        }
    }
}
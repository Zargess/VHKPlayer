using System.Collections.Generic;
using System.Linq;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer {
    public class VideoSelectionStrategy : IFileSelectionStrategy {

        public IFileSelectionStrategy PicSelection { get; private set; }

        public VideoSelectionStrategy(IFileSelectionStrategy picSelection) {
            PicSelection = picSelection;
        }

        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            var temp = SettingsManagement.GetStringSetting("playerFolders").Split(';');
            var vidFolderPath = PathHandler.AbsolutePath(temp[1]);
            if (!playable.GetContent().Any(x => x.FullPath.ToLower().Contains(vidFolderPath))) return PicSelection.SelectFiles(playable);
            res.Enqueue(playable.GetContent().Single(x => x.FullPath.ToLower().Contains(vidFolderPath)));
            return res;
        }
    }
}
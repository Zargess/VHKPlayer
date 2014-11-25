using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer {
    public class PictureSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            var file = new FileNode(SettingsManagement.Instance.GetPathSetting("playerFolders", 0));
            res.Enqueue(playable.GetContent()[0]);
            return res;
        }
    }
}

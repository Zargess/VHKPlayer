using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement
{
    public interface IPlayable {
        string Name { get; }
        Queue<IFile> Play(IFileSelectionStrategy selection);
        List<IFile> GetContent();
    }
}

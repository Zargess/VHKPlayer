using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IVHKPlayer {
        IGlobalConfigService FolderConfigService { get; }
        IGlobalConfigService GuiConfigService { get; }
    }
}

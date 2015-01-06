using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;
using VHKPlayer.Settings;

namespace VHKPlayer.Facade {
    public class VHKPlayerImpl : IVHKPlayer {
        public IGlobalConfigService FolderConfigService { get; private set; }

        public IGlobalConfigService GuiConfigService {
            get {
                throw new NotImplementedException();
            }
        }

        public VHKPlayerImpl() {
            FolderConfigService = new GlobalConfigService(new FolderSettings());
        }
    }
}

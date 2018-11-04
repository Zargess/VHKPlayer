using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VHKPlayer.Utility.Audio.Interfaces
{
    public interface IFadeAudioStrategy
    {
        void Fadein(MediaElement element);
        void Fadeout(MediaElement element);
        void StopFade();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.ViewModels.Interfaces
{
    public interface ISettingsViewModel
    {
        string SupportedVideo { get; set; }
        string SupportedAudio { get; set; }
        string SupportedPicture { get; set; }
        string SupportedInformation { get; set; }
        string PlayerStatPictureFolder { get; set; }
        string PlayerStatVideoFolder { get; set; }
        string PlayerStatMusicFolder { get; set; }
        string IgnoreFolders { get; set; }
        string PlayableFileFolders { get; set; }
        string PlayLists { get; set; }
        string AutoPlaylist { get; set; }
        string RootFolder { get; set; }
        string PlayerStatsInformationFolder { get; set; }
        string Tabs { get; set; }
        int StatTimerDelay { get; set; }
        string GoalPlayList { get; set; }
        double MusciVolume { get; set; }
        double VideoVolume { get; set; }
    }
}
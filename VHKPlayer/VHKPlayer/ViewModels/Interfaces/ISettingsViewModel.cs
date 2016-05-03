using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.ViewModels.Interfaces
{
    public interface ISettingsViewModel : INotifyPropertyChanged
    {
        string SupportedVideo { get; set; }
        string SupportedAudio { get; set; }
        string SupportedPicture { get; set; }
        string SupportedInformation { get; set; }
        string PlayerPictureFolder { get; set; }
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
        double MusicVolume { get; set; }
        double VideoVolume { get; set; }
        int Screen { get; set; }
        bool FullScreen { get; set; }
    }
}
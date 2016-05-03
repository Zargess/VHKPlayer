using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.ChangeSetting;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetBoolSetting;
using VHKPlayer.Queries.GetDoubleSetting;
using VHKPlayer.Queries.GetIntSetting;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.ViewModels.Interfaces;

namespace VHKPlayer.ViewModels
{
    public class SettingsViewModel : ISettingsViewModel, ISettingsObserver, INotifyPropertyChanged
    {
        private readonly ICommandProcessor _cprocessor;
        private readonly IQueryProcessor _qprocessor;
        private readonly Dictionary<string, string> _settings;

        #region Properties
        public string SupportedVideo
        {
            get
            {
                return GetStringSettingValue(Constants.SupportedVideoSettingName);
            }

            set
            {
                ChangeSetting(Constants.SupportedVideoSettingName, value);
                RaisePropertyChanged(nameof(SupportedVideo));
            }
        }

        public string SupportedAudio
        {
            get
            {
                return GetStringSettingValue(Constants.SupportedAudioSettingName);
            }

            set
            {
                ChangeSetting(Constants.SupportedAudioSettingName, value);
                RaisePropertyChanged(nameof(SupportedAudio));
            }
        }

        public string SupportedPicture
        {
            get
            {
                return GetStringSettingValue(Constants.SupportedPictureSettingName);
            }

            set
            {
                ChangeSetting(Constants.SupportedPictureSettingName, value);
                RaisePropertyChanged(nameof(SupportedPicture));
            }
        }

        public string SupportedInformation
        {
            get
            {
                return GetStringSettingValue(Constants.SupportedInfoFileSettingName);
            }

            set
            {
                ChangeSetting(Constants.SupportedInfoFileSettingName, value);
                RaisePropertyChanged(nameof(SupportedInformation));
            }
        }

        public string PlayerPictureFolder
        {
            get
            {
                return GetStringSettingValue(Constants.PlayerPictureFolderSettingName);
            }

            set
            {
                ChangeSetting(Constants.PlayerPictureFolderSettingName, value);
                RaisePropertyChanged(nameof(PlayerPictureFolder));
            }
        }

        public string PlayerStatPictureFolder
        {
            get
            {
                return GetStringSettingValue(Constants.PlayerStatPictureFolderSettingName);
            }

            set
            {
                ChangeSetting(Constants.PlayerStatPictureFolderSettingName, value);
                RaisePropertyChanged(nameof(PlayerStatPictureFolder));
            }
        }

        public string PlayerStatVideoFolder
        {
            get
            {
                return GetStringSettingValue(Constants.PlayerStatVideoFolderSettingName);
            }

            set
            {
                ChangeSetting(Constants.PlayerStatVideoFolderSettingName, value);
                RaisePropertyChanged(nameof(PlayerStatVideoFolder));
            }
        }

        public string PlayerStatMusicFolder
        {
            get
            {
                return GetStringSettingValue(Constants.PlayerStatMusicFolderSettingName);
            }

            set
            {
                ChangeSetting(Constants.PlayerStatMusicFolderSettingName, value);
                RaisePropertyChanged(nameof(PlayerStatMusicFolder));
            }
        }

        public string IgnoreFolders
        {
            get
            {
                return GetStringSettingValue(Constants.IgnoredFolderPathSettingName);
            }

            set
            {
                ChangeSetting(Constants.IgnoredFolderPathSettingName, value);
                RaisePropertyChanged(nameof(IgnoreFolders));
            }
        }

        public string PlayableFileFolders
        {
            get
            {
                return GetStringSettingValue(Constants.PlayableFileFoldersSettingName);
            }

            set
            {
                ChangeSetting(Constants.PlayableFileFoldersSettingName, value);
                RaisePropertyChanged(nameof(PlayableFileFolders));
            }
        }

        public string PlayLists
        {
            get
            {
                return GetStringSettingValue(Constants.PlayListsSettingName);
            }

            set
            {
                ChangeSetting(Constants.PlayListsSettingName, value);
                RaisePropertyChanged(nameof(PlayLists));
            }
        }

        public string AutoPlaylist
        {
            get
            {
                return GetStringSettingValue(Constants.AutoPlayListSettingName);
            }

            set
            {
                ChangeSetting(Constants.AutoPlayListSettingName, value);
                RaisePropertyChanged(nameof(AutoPlaylist));
            }
        }

        public string RootFolder
        {
            get
            {
                return GetStringSettingValue(Constants.RootFolderPathSettingName);
            }

            set
            {
                ChangeSetting(Constants.RootFolderPathSettingName, value);
                RaisePropertyChanged(nameof(RootFolder));
            }
        }

        public string PlayerStatsInformationFolder
        {
            get
            {
                return GetStringSettingValue(Constants.PlayerStatisticInformation);
            }

            set
            {
                ChangeSetting(Constants.PlayerStatisticInformation, value);
                RaisePropertyChanged(nameof(PlayerStatsInformationFolder));
            }
        }

        public string Tabs
        {
            get
            {
                return GetStringSettingValue(Constants.TabsSettingName);
            }

            set
            {
                ChangeSetting(Constants.TabsSettingName, value);
                RaisePropertyChanged(nameof(Tabs));
            }
        }

        public int StatTimerDelay
        {
            get
            {
                return GetIntSettingValue(Constants.StatTimerSettingName);
            }

            set
            {
                ChangeSetting(Constants.StatTimerSettingName, value);
                RaisePropertyChanged(nameof(StatTimerDelay));
            }
        }

        public string GoalPlayList
        {
            get
            {
                return GetStringSettingValue(Constants.GoalPlayList);
            }

            set
            {
                ChangeSetting(Constants.GoalPlayList, value);
                RaisePropertyChanged(nameof(GoalPlayList));
            }
        }

        public double MusicVolume
        {
            get
            {
                return GetDoubleSettingValue(Constants.MusicVolumeSettingName);
            }

            set
            {
                ChangeSetting(Constants.MusicVolumeSettingName, value);
                RaisePropertyChanged(nameof(MusicVolume));
            }
        }

        public double VideoVolume
        {
            get
            {
                return GetDoubleSettingValue(Constants.VideoVolumeSettingName);
            }

            set
            {
                ChangeSetting(Constants.VideoVolumeSettingName, value);
                RaisePropertyChanged(nameof(VideoVolume));
            }
        }

        public int Screen
        {
            get
            {
                return GetIntSettingValue(Constants.ScreenSettingName);
            }

            set
            {
                ChangeSetting(Constants.ScreenSettingName, value);
                RaisePropertyChanged(nameof(Screen));
            }
        }

        public bool FullScreen
        {
            get
            {
                return GetBoolSettingValue(Constants.FullScreenSettingName);
            }

            set
            {
                ChangeSetting(Constants.FullScreenSettingName, value);
                RaisePropertyChanged(nameof(FullScreen));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(ICommandProcessor cprocessor, IQueryProcessor qprocessor)
        {
            _cprocessor = cprocessor;
            _qprocessor = qprocessor;
            _settings = new Dictionary<string, string>();
            InitSettingsDictionary();
        }

        public void SettingsChanged(string settingName)
        {
            RaisePropertyChanged(_settings[settingName]);
        }

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #region Initiate settings dictionary
        private void InitSettingsDictionary()
        {
            _settings.Add(Constants.PlayerPictureFolderSettingName, nameof(PlayerPictureFolder));
            _settings.Add(Constants.PlayerStatPictureFolderSettingName, nameof(PlayerStatPictureFolder));
            _settings.Add(Constants.PlayerStatVideoFolderSettingName, nameof(PlayerStatVideoFolder));
            _settings.Add(Constants.PlayerStatMusicFolderSettingName, nameof(PlayerStatMusicFolder));
            _settings.Add(Constants.PlayerStatisticInformation, nameof(PlayerStatsInformationFolder));
            _settings.Add(Constants.RootFolderPathSettingName, nameof(RootFolder));
            _settings.Add(Constants.IgnoredFolderPathSettingName, nameof(IgnoreFolders));
            _settings.Add(Constants.PlayableFileFoldersSettingName, nameof(PlayableFileFolders));
            _settings.Add(Constants.PlayListsSettingName, nameof(PlayLists));
            _settings.Add(Constants.SupportedVideoSettingName, nameof(SupportedVideo));
            _settings.Add(Constants.SupportedAudioSettingName, nameof(SupportedAudio));
            _settings.Add(Constants.SupportedPictureSettingName, nameof(SupportedPicture));
            _settings.Add(Constants.SupportedInfoFileSettingName, nameof(SupportedInformation));
            _settings.Add(Constants.AutoPlayListSettingName, nameof(AutoPlaylist));
            _settings.Add(Constants.StatTimerSettingName, nameof(StatTimerDelay));
            _settings.Add(Constants.GoalPlayList, nameof(GoalPlayList));
            _settings.Add(Constants.MusicVolumeSettingName, nameof(MusicVolume));
            _settings.Add(Constants.VideoVolumeSettingName, nameof(VideoVolume));
            _settings.Add(Constants.TabsSettingName, nameof(Tabs));
            _settings.Add(Constants.ScreenSettingName, nameof(Screen));
            _settings.Add(Constants.FullScreenSettingName, nameof(FullScreen));
        }
        #endregion

        #region Settings command and queries
        private string GetStringSettingValue(string settingName)
        {
            return _qprocessor.Process(new GetStringSettingQuery
            {
                SettingName = settingName
            });
        }

        private int GetIntSettingValue(string settingName)
        {
            return _qprocessor.Process(new GetIntSettingQuery
            {
                SettingName = settingName
            });
        }

        private double GetDoubleSettingValue(string settingName)
        {
            return _qprocessor.Process(new GetDoubleSettingQuery
            {
                SettingName = settingName
            });
        }

        private bool GetBoolSettingValue(string settingName)
        {
            return _qprocessor.Process(new GetBoolSettingQuery
            {
                SettingName = settingName
            });
        }

        private void ChangeSetting<T>(string settingName, T value)
        {
            _cprocessor.ProcessTransaction(new ChangeSettingCommand
            {
                SettingName = settingName,
                Value = value
            });
        }
        #endregion
    }
}

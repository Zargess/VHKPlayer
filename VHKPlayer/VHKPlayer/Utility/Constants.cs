using System.Collections.Generic;

namespace VHKPlayer.Utility
{
    public class Constants
    {
        public const string PlayerPictureFolderSettingName = "folder_playerPictureFolder";
        public const string PlayerStatPictureFolderSettingName = "folder_playerStatPictureFolder";
        public const string PlayerStatVideoFolderSettingName = "folder_playerStatVideoFolder";
        public const string PlayerStatMusicFolderSettingName = "folder_playerStatMusicFolder";
        public const string PlayerStatisticInformation = "folder_playerStatisticInformation";
        public const string RootFolderPathSettingName = "folder_root";
        public const string IgnoredFolderPathSettingName = "folder_ignore";
        public const string PlayableFileFoldersSettingName = "folder_playableFileFolders";
        public const string PlayListsSettingName = "folder_playlists";

        public const string SupportedVideoSettingName = "folder_supportedVideo";
        public const string SupportedAudioSettingName = "folder_supportedAudio";
        public const string SupportedPictureSettingName = "folder_supportedPicture";
        public const string SupportedInfoFileSettingName = "folder_supportedInfoFile";

        public const string AutoPlayListSettingName = "application_autoPlaylist";
        public const string StatTimerSettingName = "application_statTimerDelay";
        public const string GoalPlayList = "application_goalPlayList";

        public const string MusicVolumeSettingName = "application_musicVolume";
        public const string VideoVolumeSettingName = "application_videoVolume";

        public const string TabsSettingName = "application_tabs";

        public static readonly List<string> PlayerFolderPathSettingNames = new List<string>()
        {
            PlayerPictureFolderSettingName,
            PlayerStatMusicFolderSettingName,
            PlayerStatPictureFolderSettingName,
            PlayerStatVideoFolderSettingName
        };
    }
}

using System.Collections.Generic;

namespace VHKPlayer.Utility
{
    public class Constants
    {
        public const string PlayerPictureFolderSettingName = "folder_playerPictureFolder";
        public const string PlayerVideoFolderSettingName = "folder_playerVideoFolder";
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

        public const string AutoPlayListSettingName = "application_autoPlaylist";

        public const string LeftBlockTabsSettingName = "application_leftBlockTabs";
        public const string DuringMatchTabsSettingName = "application_duringMatchTabs";
        public const string DuringMatchRightContentSettingName = "application_duringMatchRightContent";
        public const string RightBlockTabsSettingName = "application_rightBlockTabs";

        public static List<string> PlayerFolderPathSettingNames = new List<string>()
        {
            PlayerPictureFolderSettingName,
            PlayerVideoFolderSettingName,
            PlayerStatMusicFolderSettingName,
            PlayerStatPictureFolderSettingName,
            PlayerStatVideoFolderSettingName
        };
    }
}

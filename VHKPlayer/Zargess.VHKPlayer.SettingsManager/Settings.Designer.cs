﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zargess.VHKPlayer.SettingsManager {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("jpg;png")]
        public string supportedPicture {
            get {
                return ((string)(this["supportedPicture"]));
            }
            set {
                this["supportedPicture"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("avi;mp4")]
        public string supportedVideo {
            get {
                return ((string)(this["supportedVideo"]));
            }
            set {
                this["supportedVideo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mp3")]
        public string supportedMusic {
            get {
                return ((string)(this["supportedMusic"]));
            }
            set {
                this["supportedMusic"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string root {
            get {
                return ((string)(this["root"]));
            }
            set {
                this["root"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{RekFørKamp;root\\Rek;1},{RekHalvej1;root\\Rek;2}")]
        public string allFilesSortedPlayLists {
            get {
                return ((string)(this["allFilesSortedPlayLists"]));
            }
            set {
                this["allFilesSortedPlayLists"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{10sek;root\\10sek}")]
        public string iteratedFolderPlayLists {
            get {
                return ((string)(this["iteratedFolderPlayLists"]));
            }
            set {
                this["iteratedFolderPlayLists"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("xml")]
        public string supportedInfo {
            get {
                return ((string)(this["supportedInfo"]));
            }
            set {
                this["supportedInfo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("root\\spiller;root\\spillervideo;root\\spillervideostat;root\\spillervideostat\\mp3;ro" +
            "ot\\spillervideostat\\video")]
        public string playerFolders {
            get {
                return ((string)(this["playerFolders"]));
            }
            set {
                this["playerFolders"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string statsFolder {
            get {
                return ((string)(this["statsFolder"]));
            }
            set {
                this["statsFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("root\\rek;root\\10sek;root\\spiller;root\\spillervideo;root\\spillervideostat")]
        public string requiredFolders {
            get {
                return ((string)(this["requiredFolders"]));
            }
            set {
                this["requiredFolders"] = value;
            }
        }
    }
}

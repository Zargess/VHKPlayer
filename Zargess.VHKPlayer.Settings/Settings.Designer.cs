﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zargess.VHKPlayer.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
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
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string statfolder {
            get {
                return ((string)(this["statfolder"]));
            }
            set {
                this["statfolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("temp,Arkiv - Sct Mathias Centret - Benyttes igen efter, originaler,normalized,ark" +
            "iv,konverteret,spiller,spillervideo,spillervideostat")]
        public string limits {
            get {
                return ((string)(this["limits"]));
            }
            set {
                this["limits"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("musik,rek,spiller,spillervideo,spillervideostat,10sek,scorrek,foerkamp")]
        public string requiredFolders {
            get {
                return ((string)(this["requiredFolders"]));
            }
            set {
                this["requiredFolders"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("rek,{RekFørKamp;1},{RekHalvej1;2},{RekHalvej2;3},{RekEfterKamp;4}")]
        public string sortedPlayLists {
            get {
                return ((string)(this["sortedPlayLists"]));
            }
            set {
                this["sortedPlayLists"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{10sek;10sek},{ScorRek;ScorRek},{FørKamp;FoerKamp}")]
        public string specialPlayLists {
            get {
                return ((string)(this["specialPlayLists"]));
            }
            set {
                this["specialPlayLists"] = value;
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
        [global::System.Configuration.DefaultSettingValueAttribute("ScorRek")]
        public string postStatPlayList {
            get {
                return ((string)(this["postStatPlayList"]));
            }
            set {
                this["postStatPlayList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int scorlabel_x {
            get {
                return ((int)(this["scorlabel_x"]));
            }
            set {
                this["scorlabel_x"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int scorlabel_y {
            get {
                return ((int)(this["scorlabel_y"]));
            }
            set {
                this["scorlabel_y"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int penaltylabel_x {
            get {
                return ((int)(this["penaltylabel_x"]));
            }
            set {
                this["penaltylabel_x"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int penaltylabel_y {
            get {
                return ((int)(this["penaltylabel_y"]));
            }
            set {
                this["penaltylabel_y"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int savedlabel_x {
            get {
                return ((int)(this["savedlabel_x"]));
            }
            set {
                this["savedlabel_x"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int savedlabel_y {
            get {
                return ((int)(this["savedlabel_y"]));
            }
            set {
                this["savedlabel_y"] = value;
            }
        }
    }
}

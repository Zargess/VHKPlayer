﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zargess.VHKPlayer.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class GeneralSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static GeneralSettings defaultInstance = ((GeneralSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new GeneralSettings())));
        
        public static GeneralSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string inDir {
            get {
                return ((string)(this["inDir"]));
            }
            set {
                this["inDir"] = value;
            }
        }
    }
}

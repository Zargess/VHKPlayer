﻿using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Utility.Settings
{
    public class Setting : ISetting
    {
        public object this[string propertyName]
        {
            get { return Properties.Settings.Default[propertyName]; }

            set { Properties.Settings.Default[propertyName] = value; }
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Models.Interfaces
{
    public interface ISettingsObserver
    {
        void SettingsChanged(string settingName);
    }
}
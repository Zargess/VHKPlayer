using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using VHKPlayer.Queries.GetIntSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.Audio.Interfaces;

namespace VHKPlayer.Utility.Audio
{
    public class FadeAudioStrategy : IFadeAudioStrategy
    {
        private readonly IQueryProcessor processor;
        private readonly string FADEINKEY = "application_fadein_time";
        private readonly string FADEOUTKEY = "application_fadeout_time";
        private readonly string MINVOLUMEKEY = "application_minVolume";
        private readonly string MAXVOLUMEKEY = "application_maxVolume";

        private DoubleAnimation currentAnimation;

        public FadeAudioStrategy(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public void Fadein(MediaElement element)
        {
            int duration = processor.Process(new GetIntSettingQuery { SettingName = FADEINKEY });
            int min = processor.Process(new GetIntSettingQuery { SettingName = MINVOLUMEKEY });
            int max = processor.Process(new GetIntSettingQuery { SettingName = MAXVOLUMEKEY });

            currentAnimation = new DoubleAnimation(min, max, new Duration(TimeSpan.FromSeconds(duration)));

            element.BeginAnimation(MediaElement.VolumeProperty, currentAnimation);
        }

        public void Fadeout(MediaElement element)
        {
            int duration = processor.Process(new GetIntSettingQuery { SettingName = FADEOUTKEY });
            int min = processor.Process(new GetIntSettingQuery { SettingName = MINVOLUMEKEY });
            int max = processor.Process(new GetIntSettingQuery { SettingName = MAXVOLUMEKEY });

            currentAnimation = new DoubleAnimation(max, min, new Duration(TimeSpan.FromSeconds(duration)));

            element.BeginAnimation(MediaElement.VolumeProperty, currentAnimation);
        }

        public void StopFade()
        {
            if (currentAnimation == null) return;

            currentAnimation.Freeze();
        }
    }
}

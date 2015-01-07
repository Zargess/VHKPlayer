using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Sound {
    public class FadeSoundStrategy : ISoundStrategy {
        private Storyboard _fadeIn;
        private Storyboard _fadeOut;
        private Action _callback;
        private Slider _slider;
        
        public FadeSoundStrategy(Slider soundslider) {
            _slider = soundslider;
            _fadeIn = CreateBoard(100.0);
            _fadeOut = CreateBoard(0.0);
        }

        private Storyboard CreateBoard(double targetValue) {
            var res = new Storyboard();
            var animation = new DoubleAnimation();
            var time = (int)App.GuiConfigService.Get("fadeDuration");
            var duration = new Duration(TimeSpan.FromSeconds(time));
            animation.Duration = duration;
            animation.To = targetValue;

            res.Duration = duration;
            res.Children.Add(animation);
            res.Completed += AnimationComplete;

            Storyboard.SetTarget(animation, _slider);
            Storyboard.SetTargetProperty(animation, new PropertyPath(RangeBase.ValueProperty));
            return res;
        }

        private void AnimationComplete(object sender, EventArgs e) {
            if (_callback == null) return;
            _callback();
            _callback = null;
        }

        public void Starting() {
            _fadeIn.Begin();
        }

        public void Stoping(Action callBack) {
            _callback = callBack;
            _fadeOut.Begin();
        }
    }
}
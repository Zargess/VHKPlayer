using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VHKPlayer.Controls
{
    /// <summary>
    /// Interaction logic for MediaViewControl.xaml
    /// </summary>
    public partial class MediaViewControl : UserControl
    {
        public MediaViewControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StatsEnabledProperty = DependencyProperty.Register("StatsEnabled", typeof(bool), typeof(MediaViewControl), new PropertyMetadata(false));
        public static readonly DependencyProperty SoundEnabledProperty = DependencyProperty.Register("SoundEnabled", typeof(bool), typeof(MediaViewControl), new PropertyMetadata(false));

        public bool StatsEnabled
        {
            get
            {
                return (bool)GetValue(StatsEnabledProperty);
            }
            set
            {
                SetValue(StatsEnabledProperty, value);
            }
        }

        public bool SoundEnabled
        {
            get
            {
                return (bool)GetValue(SoundEnabledProperty);
            }
            set
            {
                SetValue(SoundEnabledProperty, value);
            }
        }
    }
}

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
        public static readonly DependencyProperty SavingPlacementProperty = DependencyProperty.Register("SavingPlacement", typeof(Thickness), typeof(MediaViewControl), new PropertyMetadata(new Thickness(0.0,0.0,0.0,0.0)));
        public static readonly DependencyProperty ScoringPlacementProperty = DependencyProperty.Register("ScoringPlacement", typeof(Thickness), typeof(MediaViewControl), new PropertyMetadata(new Thickness(0.0, 0.0, 0.0, 0.0)));
        public static readonly DependencyProperty PenaltyPlacementProperty = DependencyProperty.Register("PenaltyPlacement", typeof(Thickness), typeof(MediaViewControl), new PropertyMetadata(new Thickness(0.0, 0.0, 0.0, 0.0)));
        public static readonly DependencyProperty TextBlockMarginProperty = DependencyProperty.Register("TextBlockMargin", typeof(Thickness), typeof(MediaViewControl), new PropertyMetadata(new Thickness(0.0, 0.0, 0.0, 0.0)));
        public static readonly DependencyProperty TextSizeProperty = DependencyProperty.Register("TextSize", typeof(double), typeof(MediaViewControl), new PropertyMetadata(12.0));

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

        public Thickness SavingPlacement
        {
            get
            {
                return (Thickness)GetValue(SavingPlacementProperty);
            }
            set
            {
                SetValue(SavingPlacementProperty, value);
            }
        }

        public Thickness ScoringPlacement
        {
            get
            {
                return (Thickness)GetValue(ScoringPlacementProperty);
            }
            set
            {
                SetValue(ScoringPlacementProperty, value);
            }
        }

        public Thickness PenaltyPlacement
        {
            get
            {
                return (Thickness)GetValue(PenaltyPlacementProperty);
            }
            set
            {
                SetValue(PenaltyPlacementProperty, value);
            }
        }

        public Thickness TextBlockMargin
        {
            get
            {
                return (Thickness)GetValue(TextBlockMarginProperty);
            }
            set
            {
                SetValue(TextBlockMarginProperty, value);
            }
        }

        public double TextSize
        {
            get
            {
                return (double)GetValue(TextSizeProperty);
            }
            set
            {
                SetValue(TextSizeProperty, value);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VHKPlayer.Commands.GUI;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Controls
{
    /// <summary>
    /// Interaction logic for CustomListbox.xaml
    /// </summary>
    public partial class PlayableListbox : UserControl
    {
        public PlayableListbox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(PlayableListbox), new PropertyMetadata(new DoNothingCommand()));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(IPlayStrategy), typeof(PlayableListbox));
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(ICollection<IPlayable>), typeof(PlayableListbox), new PropertyMetadata(new ObservableCollection<IPlayable>()));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public IPlayStrategy CommandParameter
        {
            get { return (IPlayStrategy)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ICollection<IPlayable> Data
        {
            get
            {
                return (ICollection<IPlayable>)GetValue(DataProperty);
            }
            set
            {
                SetValue(DataProperty, value);
            }
        }
    }
}

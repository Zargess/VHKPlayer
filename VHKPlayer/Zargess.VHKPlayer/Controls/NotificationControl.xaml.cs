using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Zargess.VHKPlayer.Commands;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Controls
{
    /// <summary>
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty NotificationsProperty = DependencyProperty.Register("Notifications", typeof(ObservableCollection<INotification>), typeof(NotificationControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(NotificationControl), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty ClickedCommandProperty = DependencyProperty.Register("ClickedCommand", typeof(RelayCommand), typeof(NotificationControl), new FrameworkPropertyMetadata(null));
        public NotificationControl()
        {
            InitializeComponent();
            DataContext = this;

            Notifications = new ObservableCollection<INotification>();
            ClickedCommand = new RelayCommand(Clicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<INotification> Notifications { get; private set; }

        private bool _isOpen;

        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            private set
            {
                _isOpen = value;
                PropertyChange("IsOpen");
            }
        }

        public RelayCommand ClickedCommand { get; private set; }

        public void PropertyChange(string name)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Clicked(object parameter) { IsOpen = !IsOpen; }
    }
}

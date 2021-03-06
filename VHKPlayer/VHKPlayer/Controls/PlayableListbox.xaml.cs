﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command",
            typeof(ICommand), typeof(PlayableListbox), new PropertyMetadata(new DoNothingCommand()));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(IPlayStrategy), typeof(PlayableListbox));

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data",
            typeof(ICollection<IPlayable>), typeof(PlayableListbox),
            new PropertyMetadata(new ObservableCollection<IPlayable>()));

        public static readonly RoutedEvent SelectionChangedProperty =
            EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(PlayableListbox));

        public PlayableListbox()
        {
            InitializeComponent();
            Box.SelectionChanged += Box_SelectionChanged;
        }

        private void Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(SelectionChangedProperty);
            RaiseEvent(newEventArgs);
        }

        public IPlayable SelectedItem()
        {
            return (IPlayable) Box.SelectedItem;
        }

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedProperty, value); }
            remove { RemoveHandler(SelectionChangedProperty, value); }
        }

        public IPlayStrategy CommandParameter
        {
            get { return (IPlayStrategy) GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ICollection<IPlayable> Data
        {
            get { return (ICollection<IPlayable>) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
    }
}
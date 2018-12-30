using System.Windows;
using System.Windows.Controls;
using VHKPlayer.Exceptions;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Selectors
{
    public class TabDesignSelector : DataTemplateSelector
    {
        public DataTemplate DuringMaDataTemplate { get; set; }
        public DataTemplate PlayListDataTemplate { get; set; }
        public DataTemplate DefaultDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var tab = item as ITab;

            if (tab == null)
            {
                throw new ItemIsNotATabException("The given object does not implement ITab interface. Please fix!");
            }

            if (tab is DuringMatchTab)
            {
                return DuringMaDataTemplate;
            }

            return tab.PlayListTab ? PlayListDataTemplate : DefaultDataTemplate;
        }
    }
}
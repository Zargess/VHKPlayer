using System;
using System.Windows.Forms;
using Autofac;
using VHKPlayer.Commands.Logic.ChangeSetting;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.GUI
{
    public class BrowseForStatFolderCommand : System.Windows.Input.ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Browse for mappen der indeholder stat filer";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var processor = App.Container.Resolve<ICommandProcessor>();
                processor.ProcessTransaction(new ChangeSettingCommand()
                {
                    SettingName = Constants.PlayerStatisticInformation,
                    Value = dialog.SelectedPath
                });
            }
        }
    }
}
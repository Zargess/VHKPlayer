using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreateFolderStructure;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.DataManagement
{
    public class DataMonitor : IVHKObserver<FolderNode>
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public DataMonitor(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
            InitEventListeners();
            
        }

        private void InitEventListeners()
        {
            App.Config.FolderSettingsUpdated += Config_FolderSettingsUpdated;

            commandProcessor.Process(new CreateFolderStructureCommand()
            {
                RootFolderPath = App.Config.GetString(Constants.RootFolderPath)
            });

            foreach (var folder in App.DataCenter.Folders)
            {
                folder.AddObserver(this);
            }
        }

        public void SubjectUpdated(FolderNode subject)
        {
            // TODO : Check queries to see if folder is of specific type and then update accordingly
            throw new NotImplementedException();
        }

        private void Config_FolderSettingsUpdated(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
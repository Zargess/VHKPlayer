using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreateAllPlayers;
using VHKPlayer.Commands.Logic.CreateFolderStructure;
using VHKPlayer.Commands.Logic.CreatePlayableFilesFromFilesInFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.RemovePlayableFile;
using VHKPlayer.Commands.Logic.RemovePlayer;
using VHKPlayer.Commands.Logic.ResetDataCenter;
using VHKPlayer.Commands.Logic.UpdateDataCenterByFolder;
using VHKPlayer.DataManagement.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetPlayablesAffectedByFolder;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.DataManagement
{
    public class DataMonitor : IDataMonitor, IVhkObserver<FolderNode>
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IGlobalConfigService _configService;

        public DataMonitor(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IGlobalConfigService configService)
        {
            this._commandProcessor = commandProcessor;
            this._queryProcessor = queryProcessor;
            this._configService = configService;
            InitEventListeners();    
        }

        private void InitEventListeners()
        {
            _configService.FolderSettingsUpdated += Config_FolderSettingsUpdated;

            _commandProcessor.ProcessTransaction(new CreateFolderStructureCommand()
            {
                RootFolderPath = _queryProcessor.Process(new GetStringSettingQuery() {
                    SettingName = Constants.RootFolderPathSettingName
                })
            });

            var folders = _queryProcessor.Process(new GetFoldersQuery());

            foreach (var folder in folders)
            {
                folder.AddObserver(this);
            }
        }

        public void SubjectUpdated(FolderNode subject)
        {
            _commandProcessor.ProcessTransaction(new UpdateDataCenterByFolderCommand()
            {
                Folder = subject
            });
        }

        private void Config_FolderSettingsUpdated(object sender, PropertyChangedEventArgs e)
        {
            _commandProcessor.ProcessTransaction(new ResetDataCenterCommand());
        }
    }
}
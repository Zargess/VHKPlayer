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
    public class DataMonitor : IVHKObserver<FolderNode>
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;
        private readonly IGlobalConfigService configService;

        public DataMonitor(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IGlobalConfigService configService)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
            this.configService = configService;
            InitEventListeners();
            
        }

        private void InitEventListeners()
        {
            configService.FolderSettingsUpdated += Config_FolderSettingsUpdated;

            commandProcessor.Process(new CreateFolderStructureCommand()
            {
                RootFolderPath = queryProcessor.Process(new GetStringSettingQuery() {
                    SettingName = Constants.RootFolderPathSettingName
                })
            });

            var folders = queryProcessor.Process(new GetFoldersQuery());

            foreach (var folder in folders)
            {
                folder.AddObserver(this);
            }
        }

        public void SubjectUpdated(FolderNode subject)
        {
            commandProcessor.Process(new UpdateDataCenterByFolderCommand()
            {
                Folder = subject
            });
        }

        private void Config_FolderSettingsUpdated(object sender, PropertyChangedEventArgs e)
        {
            commandProcessor.Process(new ResetDataCenterCommand());
        }
    }
}
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
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetPlayablesAffectedByFolder;
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
                RootFolderPath = configService.GetString(Constants.RootFolderPathSettingName)
            });

            var folders = queryProcessor.Process(new GetFoldersQuery());

            foreach (var folder in folders)
            {
                folder.AddObserver(this);
            }
        }

        public void SubjectUpdated(FolderNode subject)
        {
            var playables = queryProcessor.Process(new GetPlayablesAffectedByFolderQuery()
            {
                Folder = subject
            });

            var players = playables.Where(x => x is Player).Select(x => x as Player);

            foreach (var player in players)
            {
                commandProcessor.Process(new RemovePlayerCommand()
                {
                    Player = player
                });
            }

            if (players.Count() > 0)
            {
                commandProcessor.Process(new CreateAllPlayersCommand());
            }

            var playableFiles = playables.Where(x => x is PlayableFile).Select(x => x as PlayableFile);

            foreach (var playablefile in playableFiles)
            {
                commandProcessor.Process(new RemovePlayableFileCommand()
                {
                    PlayableFile = playablefile
                });
            }

            commandProcessor.Process(new CreatePlayableFilesFromFilesInFolderCommand()
            {
                Folder = subject
            });
        }

        private void Config_FolderSettingsUpdated(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
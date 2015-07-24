using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreateFolderStructure;
using VHKPlayer.Commands.Logic.CreatePlayableFilesFromFilesInFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.RemovePlayableFile;
using VHKPlayer.Commands.Logic.RemovePlayer;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayablesAffectedByFolder;
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
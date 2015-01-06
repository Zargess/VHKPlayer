﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Loading.IPlayers;
using Zargess.VHKPlayer.Strategies.Selection.IPlayers;
using Zargess.VHKPlayer.Strategies.Loading.StatisticsLoading;
using Zargess.VHKPlayer.Utility;
using Zargess.VHKPlayer.Strategies.Utils;

namespace Zargess.VHKPlayer.Factories.IPlayers {
    public class PlayerFactory : IPlayerFactory {
        private IFile File { get; set; }

        public PlayerFactory(IFile file) {
            File = file;
        }

        public IFile CreateFile() {
            return new FileNode(File.FullPath);
        }

        public string CreateName() {
            var temp = File.Name.Remove(0, 6);
            temp = temp.Replace(".png", "");
            return temp;
        }

        public int CreateNumber() {
            var temp = File.Name.Substring(0, 3);
            return GeneralFunctions.StringToInteger(temp);
        }

        public bool CreateTrainer() {
            return CreateNumber() >= 90;
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new PlayerLoadingStrategy(File);
        }

        public IStatsLoadingStrategy CreateStatsLoadingStrategy() {
            return new DigimatchStatsLoadingStrategy();
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new GeneralPlayerSelectionStrategy(CreatePicSelectionStrategy(), CreateVidSelectionStrategy(), CreateStatSelectionStrategy());
        }

        private IFileSelectionStrategy CreatePicSelectionStrategy() {
            return new PictureSelectionStrategy(new GeneralQueuePeekStrategy());
        }

        private IFileSelectionStrategy CreateVidSelectionStrategy() {
            return new VideoSelectionStrategy(CreatePicSelectionStrategy(), new GeneralQueuePeekStrategy());
        }

        private IFileSelectionStrategy CreateStatSelectionStrategy() {
            return new StatSelectionStrategy(CreatePicSelectionStrategy(), new GeneralQueuePeekStrategy());
        }
    }
}
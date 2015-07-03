using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.StatsLoading.Interfaces;

namespace VHKPlayer.Utility.StatsLoading
{
    public class StatsLoadingStrategy : IStatsLoadingStrategy
    {
        private readonly FolderNode folder;

        public StatsLoadingStrategy(FolderNode folder)
        {
            this.folder = folder;
        }

        public Statistics LoadStats(int number)
        {
            var res = new Statistics();
            var file = folder.Content.FirstOrDefault(x => x.Name.ToLower() == "vhk_" + number + "player.xml");
            if (file == null) return res;

            using (var reader = new XmlTextReader(file.FullPath))
            {
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "shot":
                            res.Goals = reader.GetAttribute("goaltot").ToInteger();
                            res.Shots = reader.GetAttribute("shottot").ToInteger();
                            break;
                        case "penalty":
                            res.YellowCard = reader.GetAttribute("yellowcard").ToInteger();
                            res.Suspension = reader.GetAttribute("suspension").ToInteger();
                            res.RedCard = reader.GetAttribute("redcard").ToInteger();
                            break;
                        case "mvshot":
                            res.Saves = reader.GetAttribute("savetot").ToInteger();
                            res.SaveAttempts = reader.GetAttribute("shottot").ToInteger();
                            break;
                    }
                }
            }
            return res;
        }
    }
}

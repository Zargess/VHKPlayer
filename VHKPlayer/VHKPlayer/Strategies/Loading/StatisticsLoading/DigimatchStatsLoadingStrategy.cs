using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Loading.StatisticsLoading {
    public class DigimatchStatsLoadingStrategy : IStatsLoadingStrategy {
        public IStatistics LoadStats(int number) {
            var res = new Statistics();
            var file = Settings.StatFolder.Content.FirstOrDefault(x => x.Name.ToLower() == "vhk_" + number + "player.xml");
            if (file == null) return res;

            using (var reader = new XmlTextReader(file.FullPath)) {
                while (reader.Read()) {
                    switch (reader.Name) {
                        case "shot":
                            res.Goals = GeneralFunctions.StringToInteger(reader.GetAttribute("goaltot"));
                            res.Shots = GeneralFunctions.StringToInteger(reader.GetAttribute("shottot"));
                            break;
                        case "penalty":
                            res.YellowCard = GeneralFunctions.StringToInteger(reader.GetAttribute("yellowcard"));
                            res.Suspension = GeneralFunctions.StringToInteger(reader.GetAttribute("suspension"));
                            res.RedCard = GeneralFunctions.StringToInteger(reader.GetAttribute("redcard"));
                            break;
                        case "mvshot":
                            res.Saves = GeneralFunctions.StringToInteger(reader.GetAttribute("savetot"));
                            res.SaveAttempts = GeneralFunctions.StringToInteger(reader.GetAttribute("shottot"));
                            break;
                    }
                }
            }
            return res;
        }
    }
}

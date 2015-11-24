using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;

namespace VHKPlayer.Utility.LoadingStrategy.Stats
{
    public class StatisticsLoadingStrategy : ILoadingStrategy<Statistics>
    {
        private readonly FolderNode _folder;
        private readonly int _number;

        public StatisticsLoadingStrategy(int number, FolderNode folder)
        {
            this._number = number;
            this._folder = folder;
        }

        public Statistics Load()
        {
            var res = new Statistics();
            var file = _folder.Content.FirstOrDefault(x => x.Name.ToLower() == "vhk_" + _number + "player.xml");
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

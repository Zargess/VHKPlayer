using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderFromStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetPlayerStats
{
    class GetPlayerStatsQueryHandler : IQueryHandler<GetPlayerStatsQuery, Statistics>
    {
        private readonly IQueryProcessor _processor;

        public GetPlayerStatsQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public Statistics Handle(GetPlayerStatsQuery query)
        {
            var res = new Statistics();
            var statFolder = Constants.PlayerStatisticInformation;

            if (String.IsNullOrEmpty(statFolder)) return res;
            

            var folder = _processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = statFolder
            });

            var file = folder.Content.FirstOrDefault(x => x.Name.ToLower() == "vhk_" + query.Player.Number + "player.xml");
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

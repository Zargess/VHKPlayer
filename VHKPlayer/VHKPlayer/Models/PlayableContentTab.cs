﻿using System.Collections.ObjectModel;
using System.Linq;
using VHKPlayer.Infrastructure;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetAllPlayables;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class PlayableContentTab : ITab
    {
        private readonly IScriptInterpreter _interpreter;
        private readonly IQueryProcessor _processor;

        public int Number { get; set; }
        public string Name { get; set; }

        public bool PlayListTab { get; set; }
        public IScript Script { get; set; }

        public TabPlacement Placement { get; set; }
        public IPlayStrategy PlayStrategy { get; set; }

        public ObservableCollection<IPlayable> Data { get; set; }

        public PlayableContentTab(IScriptInterpreter interpreter, IQueryProcessor processor)
        {
            _interpreter = interpreter;
            _processor = processor;
        }

        public void DataUpdated()
        {
            var playables =
                _processor.Process(new GetAllPlayablesQuery())
                    .Where(playable => _interpreter.Evaluate(Script, playable));
            Data.SetCollection(playables);
        }
    }
}
using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class PlayableContentTab : ITab
    {
        public string Name { get; set; }

        public bool PlayListTab { get; set; }

        public IPlayStrategy PlayStrategy { get; set; }

        public ObservableCollection<IPlayable> Data { get; set; }

        public PlayableContentTab(IEnumerable<IPlayable> playables, IScript script)
        {
            var interpreter = App.Container.Resolve<IScriptInterpreter>();
            Data = new ObservableCollection<IPlayable>(playables.AsParallel().Where(x => interpreter.Evaluate(script, x)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayManager {
    public class GeneralManagerSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> Queue { get; private set; }
        private IFileSelectionStrategy Repeat { get; set; }
        private IFileSelectionStrategy Auto10Sek { get; set; }
        private IFileSelectionStrategy NullSelection { get; set; }
        private IFileSelectionStrategy Current { get; set; }

        public GeneralManagerSelectionStrategy(Queue<IFile> queue, IFileSelectionStrategy repeat, IFileSelectionStrategy auto10Sek, IFileSelectionStrategy nullSelection) {
            Queue = queue;
            Repeat = repeat;
            Auto10Sek = auto10Sek;
            NullSelection = nullSelection;
            Current = NullSelection;
        }

        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            SetCurrent(p);
            return Current.HintNext(q, p, type);
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            SetCurrent(playable);
            return Current.SelectFiles(playable, type);
        }

        private bool Auto10SekActive() {
            return (bool)App.GuiConfigService.Get("auto10Sek");
        }

        private void SetCurrent(IPlayable p) {
            if (Queue.Count > 0) Current = NullSelection;
            else if (p.Repeat) Current = Repeat;
            else if (Auto10SekActive()) Current = Auto10Sek;
            else Current = NullSelection;
        }
    }
}

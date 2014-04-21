using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayList : IPlayAble {
        public string Name { get; private set; }
        public List<IPlayAble> Content { get; private set; }

        public PlayList(string name) {
            Name = name;
        }

        public IEnumerable<IPlayAble> GetContent() {
            var res = new List<IPlayAble>();
            foreach (var p in Content) {
                res.AddRange(p.GetContent());
            }
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Commands {
    public interface IExecuteAble {
        string Name { get; }
        int RequiredArgs { get; }
        object Run(string[] args);
    }
}

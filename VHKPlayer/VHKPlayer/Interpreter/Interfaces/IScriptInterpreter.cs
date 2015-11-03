using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interpreter.Interfaces
{
    public interface IScriptInterpreter
    {
        bool Evaluate(string script, object parameter);
    }
}

using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Interpreter.Interfaces
{
    public interface IScriptInterpreter
    {
        bool Evaluate(IScript script, object parameter);
    }
}
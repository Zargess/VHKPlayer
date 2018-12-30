using ScriptParser;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Models
{
    public class Script : IScript
    {
        public Program Code { get; private set; }

        public Script(string script)
        {
            Code = Parser.Parse(script);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Exceptions;
using VHKPlayer.Infrastructure;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.PlayListLoading;
using VHKPlayer.Utility.PlayStrategy;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Interpreter
{
    public class ScriptInterpreter : IScriptInterpreter
    {
        private readonly IQueryProcessor processor;
        private const string FOLDERSELECTOR = "(folder *)";
        private const string TYPESELECTOR = "(type *)";
        private const string PROPERTYSELECTOR = "(property * *)";

        public ScriptInterpreter(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public bool Evaluate(string script, object input)
        {
            if (Regex.IsMatch(script, FOLDERSELECTOR))
            {
                return HandleFolderScript(script, input);
            }
            else if (Regex.IsMatch(script, TYPESELECTOR))
            {
                return HandleTypeScript(script, input);
            }
            else if (Regex.IsMatch(script, PROPERTYSELECTOR))
            {
                return HandlePropertyScript(script, input);
            }
            throw new SyntaxErrorException("Script is not recognised\n" + script);
        }

        private bool HandlePropertyScript(string script, object input)
        {
            throw new NotImplementedException();
        }

        private bool HandleTypeScript(string script, object input)
        {
            throw new NotImplementedException();
        }

        private bool HandleFolderScript(string script, object input)
        {
            throw new NotImplementedException();
        }
    }
}
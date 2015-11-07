using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Exceptions;
using VHKPlayer.Infrastructure;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetFolderByRelativePath;
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
        private const string IDENTIFIERPATTERN = "[_a-zA-z][a-zA-Z0-9]*";
        private const string STRINGPATTERN = "\".*?\"";
        private const string INTEGERPATTERN = @"\d+";
        private const string BOOLEANPATTERN = "[True|False]";
        private const string VALUEPATTERN = "[" + STRINGPATTERN + "|" + INTEGERPATTERN + "|" + BOOLEANPATTERN + "]";
        private const string FOLDERSELECTOR = "(folder path:*)";
        private const string TYPESELECTOR = "(type name:*)";
        private const string PROPERTYSELECTOR = "(property name:" + IDENTIFIERPATTERN + " value:" + VALUEPATTERN + ")";

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
            var identifierMatch = Regex.Match(script, "name:" + IDENTIFIERPATTERN);
            var valueMatch = Regex.Match(script, "value:" + VALUEPATTERN);
            if (!identifierMatch.Success) throw new SyntaxErrorException("A Property selector must have a legal property name!\n" + IDENTIFIERPATTERN + "\n" + script);
            if (!valueMatch.Success) throw new SyntaxErrorException("A Property selector must have a legal value!\n" + VALUEPATTERN + "\n" + script);

            throw new NotImplementedException();
        }

        private bool HandleTypeScript(string script, object input)
        {
            var match = Regex.Match(script, "name:" + IDENTIFIERPATTERN);
            if (!match.Success) throw new SyntaxErrorException("Type must not be seperated by space and should not allow special characters or allow starting with number.\n" + script);

            var parameter = match.Value.Replace("name:", "");
            try
            {
                var t = Type.GetType("VHKPlayer.Models." + parameter);
                var temp = t == input.GetType();
                return t == input.GetType();
            }
            catch (TypeLoadException ex)
            {
                Console.WriteLine("Type {0} was not recognised as a type.\n{1}", parameter, ex.StackTrace);
            }

            return false;
        }

        private bool HandleFolderScript(string script, object input)
        {
            var match = Regex.Match(script, STRINGPATTERN);
            if (!match.Success) throw new SyntaxErrorException("A folder selector must have the path in quotes!\n" + script);

            var playablefile = input as PlayableFile;
            if (playablefile == null) return false; // Not a playable file!

            var relativePath = match.Value.Replace("\"", "");

            var folder = processor.Process(new GetFolderByRelativePathQuery()
            {
                RelativePath = relativePath
            });

            return folder.Contains(playablefile.File);
        }
    }
}
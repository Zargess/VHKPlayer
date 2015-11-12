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
        private const string BOOLEANPATTERN = "True|False";
        private const string VALUEPATTERN = "(?:" + STRINGPATTERN + "|" + INTEGERPATTERN + "|" + BOOLEANPATTERN + ")";
        private const string FOLDERSELECTOR = "(folder path:*)";
        private const string TYPESELECTOR = "(type name:*)";
        private const string PROPERTYSELECTOR = "(property * *)";
        private const string MULTISELECTOR = "(multi * *)";
        //private const string SCRIPTPATTERN = "[?:\b" + FOLDERSELECTOR + "\b|\b" + TYPESELECTOR + "\b|\b" + PROPERTYSELECTOR + "\b|\b" + MULTISELECTOR + "\b]";
        //private const string SCRIPTPATTERN = "(?:\bfolder\b|\btype\b|\bproperty\b|\bmulti\b)";
        private const string SCRIPTPATTERN = @"(?<=\().+?(?=\))";

        public ScriptInterpreter(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public bool Evaluate(string script, object input)
        {
            if (Regex.IsMatch(script, MULTISELECTOR))
            {
                return HandleMultiScript(script, input);
            }
            else if (Regex.IsMatch(script, FOLDERSELECTOR))
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

        private bool HandleMultiScript(string script, object input)
        {
            var leftMatch = Regex.Match(script, SCRIPTPATTERN);
            var rightMatch = Regex.Match(script.Remove(0,7), "right:*");
            
            if (!leftMatch.Success) throw new SyntaxErrorException("The left script is not valid!\n" + script);
            if (!rightMatch.Success) throw new SyntaxErrorException("The right script is not valid!\n" + script);

            throw new NotImplementedException();
        }

        private bool HandlePropertyScript(string script, object input)
        {
            var identifierMatch = Regex.Match(script, "name:" + IDENTIFIERPATTERN);
            var valueMatch = Regex.Match(script, "value:" + VALUEPATTERN);
            if (!identifierMatch.Success) throw new SyntaxErrorException("A Property selector must have a legal property name!\n" + IDENTIFIERPATTERN + "\n" + script);
            if (!valueMatch.Success) throw new SyntaxErrorException("A Property selector must have a legal value!\n" + VALUEPATTERN + "\n" + script);

            var identifier = identifierMatch.Value.Replace("name:", "");
            var value = valueMatch.Value.Replace("value:", "");

            var property = input.GetType().GetProperty(identifier);
            if (property == null) return false;

            var propertyType = property.PropertyType;
            var propertyIsString = propertyType == typeof(string);
            var propertyIsInt = propertyType == typeof(int);
            var propertyIsBool = propertyType == typeof(bool);
            var stringMatch = Regex.IsMatch(value, STRINGPATTERN);
            var intMatch = Regex.IsMatch(value, INTEGERPATTERN);
            var boolMatch = Regex.IsMatch(value, BOOLEANPATTERN);

            if (propertyIsString && stringMatch)
            {
                var temp = value.Replace("\"", "");
                var inputValue = property.GetValue(input) as string;
                return temp.Equals(inputValue);
            }

            if (propertyIsInt && intMatch)
            {
                var temp = value.ToInteger();
                var inputValue = (int)property.GetValue(input);
                return temp == inputValue;
            }

            if (propertyIsBool && boolMatch)
            {
                var temp = value.ToBool();
                var test = property.GetValue(input);
                var inputValue = (bool)property.GetValue(input);
                return temp == inputValue;
            }

            throw new SyntaxErrorException("Value is not of legal type. Only string, int and bool are supported!\n" + value);
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
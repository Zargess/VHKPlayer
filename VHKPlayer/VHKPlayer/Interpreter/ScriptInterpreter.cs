using System;
using ScriptParser;
using VHKPlayer.Exceptions;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetFolderByRelativePath;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Interpreter
{
    public class ScriptInterpreter : IScriptInterpreter
    {
        private readonly IQueryProcessor _processor;

        public ScriptInterpreter(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public bool Evaluate(IScript script, object input)
        {
            return Evaluate(script.Code, input);
        }

        private bool Evaluate(Program program, object input)
        {
            if (program.IsFolder) return HandleFolderScript(program, input);
            if (program.IsType) return HandleTypeScript(program, input);
            if (program.IsProperty) return HandlePropertyScript(program, input);
            if (program.IsMulti) return HandleMultiScript(program, input);
            if (program.IsError) return HandleErrorScript(program, input);

            throw new SyntaxErrorException("Script not recognised\n" + program.ToString());
        }

        private bool HandleErrorScript(Program program, object input)
        {
            var errorScript = (Program.Error) program;
            throw new SyntaxErrorException(errorScript.Item);
        }

        private bool HandleMultiScript(Program script, object input)
        {
            var multiScript = (Program.Multi) script;
            return Evaluate(multiScript.Item1, input) && Evaluate(multiScript.Item2, input);
        }

        private bool HandlePropertyScript(Program script, object input)
        {
            var propertyScript = (Program.Property) script;

            var identifier = propertyScript.Item1;
            var value = propertyScript.Item2;

            var property = input.GetType().GetProperty(identifier);
            if (property == null) return false;

            var propertyType = property.PropertyType;
            var propertyIsString = propertyType == typeof(string);
            var propertyIsInt = propertyType == typeof(int);
            var propertyIsBool = propertyType == typeof(bool);
            var stringMatch = value.GetType() == typeof(string);
            var intMatch = value.GetType() == typeof(int);
            var boolMatch = value.GetType() == typeof(bool);

            if (propertyIsString && stringMatch)
            {
                var temp = (string) value;
                var inputValue = property.GetValue(input) as string;
                return temp.Equals(inputValue);
            }

            if (propertyIsInt && intMatch)
            {
                var temp = (int) value;
                var inputValue = (int) property.GetValue(input);
                return temp == inputValue;
            }

            if (propertyIsBool && boolMatch)
            {
                var temp = (bool) value;
                var test = property.GetValue(input);
                var inputValue = (bool) property.GetValue(input);
                return temp == inputValue;
            }

            throw new SyntaxErrorException("Value is not of legal type. Only string, int and bool are supported!\n" +
                                           value);
        }

        private bool HandleTypeScript(Program script, object input)
        {
            var typeScript = (Program.Type) script;
            var parameter = typeScript.Item;
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

        private bool HandleFolderScript(Program script, object input)
        {
            var folderScript = (Program.Folder) script;
            var playablefile = input as PlayableFile;
            if (playablefile == null) return false; // Not a playable file!

            var relativePath = folderScript.Item.Replace("\"", "");

            var folder = _processor.Process(new GetFolderByRelativePathQuery()
            {
                RelativePath = relativePath
            });

            return folder.Contains(playablefile.File);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;

namespace Zargess.VHKPlayer.GUI {
    public class Terminal : TextBox {
        protected enum CommandHistoryDirection { BACKWARD, FORWARD }

        public bool IsPromptInsertedAtLaunch { get; set; }
        public bool IsSystemBeepEnabled { get; set; }
        public string Prompt { get; set; }

        public List<string> RegisteredCommands { get; private set; }
        public List<Command> CommandLog { get; private set; }
        public int LastPomptIndex { get; private set; }
        public bool IsInputEnabled { get; private set; }

        private int indexInLog = 0;

        private Border Caret { get; set; }

        public Terminal() {
            IsUndoEnabled = false;
            AcceptsReturn = false;
            AcceptsTab = false;

            RegisteredCommands = new List<string>();
            CommandLog = new List<Command>();
            IsPromptInsertedAtLaunch = true;
            IsSystemBeepEnabled = true;
            LastPomptIndex = -1;
            Prompt = "\n> ";
            IsInputEnabled = false;

            Loaded += (s, e) => {
                if (IsPromptInsertedAtLaunch)
                    InsertNewPrompt();
            };

            TextChanged += (s, e) => ScrollToEnd();
        }

        public void SetNewCaret(Border c) {
            Caret = c;

            SelectionChanged += (sender, e) => MoveCustomCaret();
            LostFocus += (sender, e) => Caret.Visibility = Visibility.Hidden;
            GotFocus += (sender, e) => Caret.Visibility = Visibility.Visible;
            SizeChanged += (sender, e) => MoveCustomCaret();
        }

        private void MoveCustomCaret() {
            var caretLocation = GetRectFromCharacterIndex(CaretIndex).Location;

            if (!double.IsInfinity(caretLocation.X)) {
                Canvas.SetLeft(Caret, caretLocation.X);
            }

            if (!double.IsInfinity(caretLocation.Y)) {
                Canvas.SetTop(Caret, caretLocation.Y);
            }
        }

        // --------------------------------------------------------------------
        // PUBLIC INTERFACE
        // --------------------------------------------------------------------

        public void InsertNewPrompt() {
            if (Text.Length > 0)
                Text += Text.EndsWith("\n") ? "" : "\n";
            Text += Prompt;
            CaretIndex = Text.Length;
            LastPomptIndex = Text.Length;
            IsInputEnabled = true;
        }

        public void InsertLineBeforePrompt(string text) {
            var oldPromptIndex = LastPomptIndex;
            var insertedText = text + (text.EndsWith("\n") ? "" : "\n");
            Text = Text.Insert(LastPomptIndex - Prompt.Length, insertedText);
            CaretIndex = Text.Length;
            LastPomptIndex = oldPromptIndex + insertedText.Length;
        }

        // --------------------------------------------------------------------
        // EVENT HANDLER
        // --------------------------------------------------------------------

        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            // If Ctrl+C is entered, raise an abortrequested event !
            if (e.Key == Key.C) {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) {
                    RaiseAbortRequested();
                    e.Handled = true;
                    return;
                }
            }

            // Store the length of Text before any input processing.
            var initialLength = Text.Length;

            // If input is not allowed, warn the user and discard its input.
            if (!IsInputEnabled) {
                if (IsSystemBeepEnabled)
                    SystemSounds.Beep.Play();
                e.Handled = true;
                return;
            }

            // Test the caret position.
            //
            // 1. If located before the last prompt index
            //    ==> Warn, set the caret at the end of input text, add text, discard the input
            //        if user tries to erase text, else process it.
            //
            // 2. If located at the last prompt index and user tries to erase text
            //    ==> Warn, discard the input.
            //
            // 3. If located at the last prompt index and user tries to move backward
            //    ==> Warn, discard the input.
            //
            // 4. If located after (>=) the last prompt index and user presses the UP key
            //    ==> Launch command history backward, discard the input.
            //
            // 5. If located after (>=) the last prompt index and user presses the UP key
            //    ==> Launch command history forward, discard the input.
            //
            if (CaretIndex < LastPomptIndex) {
                if (IsSystemBeepEnabled)
                    SystemSounds.Beep.Play();
                CaretIndex = Text.Length;
                e.Handled = (e.Key == Key.Back || e.Key == Key.Delete);
            } else if (CaretIndex == LastPomptIndex && e.Key == Key.Back) {
                if (IsSystemBeepEnabled)
                    SystemSounds.Beep.Play();
                e.Handled = true;
            } else if (CaretIndex == LastPomptIndex && e.Key == Key.Left) {
                if (IsSystemBeepEnabled)
                    SystemSounds.Beep.Play();
                e.Handled = true;
            } else if (CaretIndex >= LastPomptIndex && e.Key == Key.Up) {
                HandleCommandHistoryRequest(CommandHistoryDirection.BACKWARD);
                e.Handled = true;
            } else if (CaretIndex >= LastPomptIndex && e.Key == Key.Down) {
                HandleCommandHistoryRequest(CommandHistoryDirection.FORWARD);
                e.Handled = true;
            }

            // If input has not yet been discarded, test the key for special inputs.
            // ENTER   => validates the input
            // TAB     => launches command completion with registered commands
            // CTRL+C  => raises an abort request event
            if (!e.Handled) {
                switch (e.Key) {
                    case Key.Enter:
                        HandleEnterKey();
                        e.Handled = true;
                        break;
                    case Key.Tab:
                        HandleTabKey();
                        e.Handled = true;
                        break;
                }
            }

            base.OnPreviewKeyDown(e);
        }

        // --------------------------------------------------------------------
        // VIRTUAL METHODS
        // --------------------------------------------------------------------

        protected virtual void HandleCommandHistoryRequest(CommandHistoryDirection direction) {
            try {
                switch (direction) {
                    case CommandHistoryDirection.BACKWARD:
                        if (indexInLog > 0)
                            indexInLog--;
                        if (CommandLog.Count > 0) {
                            Text = GetTextWithPromptSuffix(CommandLog[indexInLog].Raw);
                            CaretIndex = Text.Length;
                        }
                        break;

                    case CommandHistoryDirection.FORWARD:
                        if (indexInLog < CommandLog.Count - 1)
                            indexInLog++;
                        if (CommandLog.Count > 0) {
                            Text = GetTextWithPromptSuffix(CommandLog[indexInLog].Raw);
                            CaretIndex = Text.Length;
                        }
                        break;
                }
            } catch (ArgumentOutOfRangeException) {}
        }

        public virtual void HandleEnterKey() {
            var line = Text.Substring(LastPomptIndex);
            Text += "\n";
            IsInputEnabled = false;
            LastPomptIndex = int.MaxValue;

            var cmd = TerminalUtils.ParseCommandLine(line);
            CommandLog.Add(cmd);
            indexInLog = CommandLog.Count;
            RaiseCommandEntered(cmd);
        }

        public void RunCommand(string command) {
            Text += command;
            var cmd = TerminalUtils.ParseCommandLine(command);
            CommandLog.Add(cmd);
            indexInLog = CommandLog.Count;
            RaiseCommandEntered(cmd);
        }

        public void AppendText(object o) {
            InsertLineBeforePrompt(o.ToString());
        }

        protected virtual void HandleTabKey() {
            // Command completion works only if caret is at last character
            // and if the user already typed something.
            if (CaretIndex != Text.Length || CaretIndex == LastPomptIndex)
                return;

            // Get command name and associated commands
            string line = Text.Substring(LastPomptIndex);
            string[] commands = GetAssociatedCommands(line);

            // If some associated command exist...
            if (commands.Length > 0) {
                // Get the commands common prefix
                string commonPrefix = GetCommonPrefix(commands);
                // If there is no more autocompletion available...
                if (commonPrefix == line) {
                    // If there are more than one command to print
                    if (commands.Length > 1) {
                        // Print every associated command and insert a new prompt
                        foreach (string cmd in commands)
                            Text += "\n" + cmd;
                        InsertNewPrompt();
                        Text += line;
                        CaretIndex = Text.Length;
                    }
                } else {
                    // Erase the user input
                    Text = Text.Remove(LastPomptIndex);
                    // Insert the common prefix
                    Text += commonPrefix;
                    // Set the caret at the end of the text
                    CaretIndex = Text.Length;
                }
                return;
            }

            // If no command exists, try path completion
            if (line.Contains("\"") && line.Split('"').Length % 2 == 0) {
                int idx = line.LastIndexOf('"');
                string prefix = line.Substring(0, idx + 1);
                string suffix = line.Substring(idx + 1, line.Length - prefix.Length);
                CompletePath(prefix, suffix);
            } else {
                int idx = Math.Max(line.LastIndexOf(' '), line.LastIndexOf('\t'));
                string prefix = line.Substring(0, idx + 1);
                string suffix = line.Substring(idx + 1, line.Length - prefix.Length);
                CompletePath(prefix, suffix);
            }
        }

        // --------------------------------------------------------------------
        // CLASS SPECIFIC UTILITIES
        // --------------------------------------------------------------------

        protected void CompletePath(string linePrefix, string lineSuffix) {
            if (lineSuffix.Contains("\\") || lineSuffix.Contains("/")) {
                var idx = Math.Max(lineSuffix.LastIndexOf("\\"), lineSuffix.LastIndexOf("/"));
                var dir = lineSuffix.Substring(0, idx + 1);
                var prefix = lineSuffix.Substring(idx + 1, lineSuffix.Length - dir.Length);
                var files = GetFileList(dir, lineSuffix[idx] == '\\');

                var commonPrefixFiles = files.Where(file => file.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (commonPrefixFiles.Count > 0) {
                    var commonPrefix = GetCommonPrefix(commonPrefixFiles.ToArray());
                    if (commonPrefix == prefix) {
                        foreach (string file in commonPrefixFiles)
                            Text += "\n" + file;
                        InsertNewPrompt();
                        Text += linePrefix + lineSuffix;
                        CaretIndex = Text.Length;
                    } else {
                        Text = Text.Remove(LastPomptIndex);
                        Text += linePrefix + dir + commonPrefix;
                        CaretIndex = Text.Length;
                    }
                }
            }
        }

        protected string GetTextWithPromptSuffix(string suffix) {
            string ret = Text.Substring(0, LastPomptIndex);
            return ret + suffix;
        }

        protected string[] GetAssociatedCommands(string prefix) {
            return RegisteredCommands.Where(cmd => cmd.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase)).ToArray();
        }

        // --------------------------------------------------------------------
        // GENERAL UTILITIES
        // --------------------------------------------------------------------

        protected string GetShortestString(string[] strs) {
            var ret = strs[0];
            return strs.Aggregate(ret, (current, str) => str.Length < current.Length ? str : current);
        }

        protected string GetCommonPrefix(string[] strs) {
            var shortestStr = GetShortestString(strs);
            for (var i = 0; i < shortestStr.Length; i++)
                if (strs.Any(str => char.ToLower(str[i]) != char.ToLower(shortestStr[i]))) {
                    return shortestStr.Substring(0, i);
                }
            return shortestStr;
        }

        protected string[] GetFileList(string dir, bool useAntislash) {
            if (!Directory.Exists(dir))
                return new string[0];
            var dirs = Directory.GetDirectories(dir);
            var files = Directory.GetFiles(dir);

            for (var i = 0; i < dirs.Length; i++)
                dirs[i] = Path.GetFileName(dirs[i]) + (useAntislash ? "\\" : "/");
            for (var i = 0; i < files.Length; i++)
                files[i] = Path.GetFileName(files[i]);

            var ret = new List<string>();
            ret.AddRange(dirs);
            ret.AddRange(files);
            return ret.ToArray();
        }

        // --------------------------------------------------------------------
        // CUSTOM EVENTS
        // --------------------------------------------------------------------

        public event EventHandler<EventArgs> AbortRequested;
        public event EventHandler<CommandEventArgs> CommandEntered;

        public class CommandEventArgs : EventArgs {
            public Command Command { get; private set; }
            public CommandEventArgs(Command command) {
                Command = command;
            }
        }

        private void RaiseAbortRequested() {
            if (AbortRequested != null)
                AbortRequested(this, new EventArgs());
        }

        private void RaiseCommandEntered(Command command) {
            if (CommandEntered != null)
                CommandEntered(this, new CommandEventArgs(command));
        }
    }
}
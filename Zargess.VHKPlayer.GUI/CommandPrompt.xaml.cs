using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Settings;
using Zargess.VHKPlayer.WebSocket;

namespace Zargess.VHKPlayer.GUI {
    /// <summary>
    ///     Interaction logic for CommandPrompt.xaml
    /// </summary>
    public partial class CommandPrompt {
        private MainViewModel MainView { get; set; }
        private WebServer Server { get; set; }

        public CommandPrompt() {
            MainView = new MainViewModel(this);
            InitializeComponent();
            Term.Prompt = "\n> ";

            Loaded += (s, e) => {
                Term.AbortRequested += (ss, ee) => MessageBox.Show("Abort !");

                Term.RegisteredCommands.Add("set-root");
                Term.RegisteredCommands.Add("load");
                Term.RegisteredCommands.Add("reload");
                Term.RegisteredCommands.Add("server");

                Term.Text += "Welcome !\n";
                Term.Text += "Hit tab to complete your current command.\n";
                Term.Text += "Use ctrl+c to raise an AbortRequested event.\n\n";
                Term.Text += "Available (fake) commands are:\n";
                Term.RegisteredCommands.ForEach(cmd => Term.Text += "  - " + cmd + "\n");
                Term.InsertNewPrompt();

                Server = new WebServer(8100);
                Server.MessageSent += (sender, ee) => Dispatcher.Invoke(() => PrintText(ee.Message));
            };
            
            Term.CommandEntered += (ss, ee) => {
                var msg = ee.Command.GetDescription("Command is '{0}'", " with args '{0}'", ", '{0}'", ".");
                Term.Text += msg;
                Term.InsertNewPrompt();
                Console.WriteLine(msg);
                CheckCommand(ee.Command);
            };
            Closing += (s, e) => Environment.Exit(0);
        }

        private void CheckCommand(Command command) {
            if (command.Name == "load") {
                MainView.LoadStructure(SettingsManager.GetSetting("root") as string);
                MainView.Video.Select(x => x).ToList().ForEach(x => PrintText(x.FullPath));
                MainView.Audio.Select(x => x).ToList().ForEach(x => PrintText(x.FullPath));
            } else if (command.Name == "set-root" && command.Args.Length == 1) {
                SettingsManager.SetSetting("root", command.Args[0]);
            } else if (command.Name == "reload" && command.Args.Length == 1) {
                var folder = new FolderNode(command.Args[0]);
                var files = MainView.Files.Where(x => x.Source == folder.Name).ToList();
                files.ForEach(x => MainView.Files.Remove(x));
                MainView.LoadStructure(folder.FullPath);
            } else if (command.Name == "server") {
                var list = new List<string>();
                for (var i = 1; i < command.Args.Length; i++) {
                    list.Add(command.Args[i]);
                }
                Server.CheckCommands(command.Args[0], list.ToArray());
            } else if (command.Name == "exit") {
                Close();
            }
        }

        public void PrintText(string text) {
            Term.InsertLineBeforePrompt(text);
        }
    }
}
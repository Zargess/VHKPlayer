using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Zargess.VHKPlayer.Utility;
using Forms = System.Windows.Forms;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.GUI.PlayManagement;
using Zargess.VHKPlayer.Settings;
using Zargess.VHKPlayer.WebSocket;
using Zargess.VHKPlayer.ViewModels;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using Zargess.VHKPlayer.Players;

namespace Zargess.VHKPlayer.GUI {
    /// <summary>
    ///     Interaction logic for CommandPrompt.xaml
    /// </summary>
    public partial class CommandPrompt {
        // TODO : Change gui to a more linux mint terminal look, because it is awesome! image link: http://www.ozone3d.net/blogs/lab/20120612/linux-mint-13-marco-et-les-terminaux-transparents/
        private MainViewModel MainVm { get; set; }
        private WebServer Server { get; set; }
        private Terminal Term { get; set; }
        private PlayManager Manager { get; set; }

        public CommandPrompt(MainViewModel mv, PlayManager manager) {
            MainVm = mv;
            InitializeComponent();
            Term = Cons.Term;
            Manager = manager;
            Loaded += (s, e) => {
                Term.AbortRequested += (ss, ee) => MessageBox.Show("Abort !");
                // TODO : Make some printing functions to show the application's current state
                Term.RegisteredCommands.Add("set-root");
                Term.RegisteredCommands.Add("set-stat-fold");
                Term.RegisteredCommands.Add("load");
                Term.RegisteredCommands.Add("reload-all");
                Term.RegisteredCommands.Add("server");
                Term.RegisteredCommands.Add("get-root");
                Term.RegisteredCommands.Add("validate");
                Term.RegisteredCommands.Add("help");
                Term.RegisteredCommands.Add("play");
                Term.RegisteredCommands.Add("continue");
                Term.RegisteredCommands.Add("pause");
                Term.RegisteredCommands.Add("stop");
                Term.RegisteredCommands.Add("hide");
                Term.RegisteredCommands.Add("terminate");

                Term.Text += "Welcome !\n";
                Term.Text += "Hit tab to complete your current command.\n";
                Term.Text += "Use ctrl+c to raise an AbortRequested event.\n\n";
                Term.Text += "Available commands are:\n";
                Term.RegisteredCommands.ForEach(cmd => Term.Text += "  - " + cmd + "\n");
                Term.InsertNewPrompt();

                Server = new WebServer(8100);
                Server.MessageSent += (sender, ee) => Dispatcher.Invoke(() => Console.WriteLine(ee.Message));
                Term.Focus();
                Console.SetOut(new ControlWriter(Term, Application.Current.Dispatcher));
            };

            Term.CommandEntered += (ss, ee) => {
                Term.InsertNewPrompt();
                CheckCommand(ee.Command);
            };
            Closing += (s, e) => {
                if (Server != null) Server.Shutdown();
            };
        }

        // TODO : Rethink the concept of reload. Should a manual reload be possible?
        private void CheckCommand(Command command) {
            var watch = Stopwatch.StartNew();
            var argLength = command.Args.Length;
            if (command.Name == "load") {
                MainVm.LoadStructure(SettingsManager.GetSetting("root") as string);
            } else if (command.Name == "set-root" && argLength == 0) {
                SetRoot();
            } else if (command.Name == "set-root" && argLength == 1) {
                SetRoot(command.Args[0]);
            } else if (command.Name == "set-stat-fold" && argLength == 1) {
                SettingsManager.SetSetting("statfolder", command.Args[0]);
            } else if (command.Name == "server") {
                ServerCommand(command);
            } else if (command.Name == "hide") {
                Close();
            } else if (command.Name == "get-root") {
                Console.WriteLine(SettingsManager.GetSetting("root"));
            } else if (command.Name == "help") {
                Console.WriteLine("Available commands are:");
                Term.RegisteredCommands.ForEach(cmd => Console.WriteLine("  - " + cmd + "\n"));
            } else if (command.Name == "validate") {
                var folder = new FolderNode(SettingsManager.GetSetting("root") as string);
                Console.WriteLine("Folder valid: {0}", folder.ValidRootFolder());
            } else if (command.Name == "terminate") {
                Application.Current.Shutdown();
            } else if (command.Name == "play" && argLength == 2) {
                Play(command.Args[0], command.Args[1]);
            } else if (command.Name == "pause" && argLength == 1) {
                PausePlayer(command.Args[0]);
            } else if (command.Name == "stop" && argLength == 1) {
                StopPlayer(command.Args[0]);
            } else if (command.Name == "continue" && argLength == 1) {
                ContinuePlayer(command.Args[0]);
            } else {
                Console.WriteLine("{0} with the given parameters is not a recordnized command.", command.Name);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time used: {0}", elapsedMs);
        }

        private void SetRoot() {
            var fbd = new Forms.FolderBrowserDialog { SelectedPath = SettingsManager.GetSetting("root") as string };
            if (fbd.ShowDialog() == Forms.DialogResult.OK) {
                SetRoot(fbd.SelectedPath);
            }
        }

        public void RunCommand(string command) {
            Term.RunCommand(command);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void SetRoot(string arg) {
            var root = new FolderNode(arg);
            if (root.ValidRootFolder()) {
                SettingsManager.SetSetting("root", arg);
            } else {
                Console.WriteLine("Cannot use {0} as a root folder.\nPlease choose another.", arg);
            }
        }

        private void ServerCommand(Command command) {
            var list = new List<string>();
            for (var i = 1; i < command.Args.Length; i++) {
                list.Add(command.Args[i]);
            }
            Server.CheckCommands(command.Args[0], list.ToArray());
        }

        private void Play(string type, string key) {
            switch (type) {
                case "Music":
                    PlayMusic(key);
                    break;
                case "File":
                    PlayVideo(key);
                    break;
                case "PlayList":
                    break;
                case "Video":
                case "Stat":
                    var player = MainVm.Players.SingleOrDefault(x => x.Number == Utils.ConvertToInt(key));
                    PlayPlayer(player,type);
                    break;
                case "People":
                    var p = MainVm.People.SingleOrDefault(x => x.Number == Utils.ConvertToInt(key));
                    PlayPlayer(p,type);
                    break;
            }
        }

        private void PlayVideo(string key) {
            if (MainVm.Video.Any(folder => folder.ContainsFile(key))) {
                Manager.Play(new FileNode(key));
            }
        }

        private void PlayMusic(string key) {
            if (MainVm.Audio.Any(folder => folder.ContainsFile(key))) {
                Manager.Play(new FileNode(key));
            }
        }

        private void PlayPlayer(Player p, string type) {
            if (p == null) return;
            Manager.Play(p,type);
        }

        private void StopPlayer(string me) {
            Manager.Stop(me);
        }

        private void PausePlayer(string me) {
            Manager.Pause(me);
        }

        private void ContinuePlayer(string me) {
            Manager.Continue(me);
        }
    }
}
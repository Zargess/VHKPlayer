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
        // TODO : Change gui to a more linux mint terminal look, because it is awesome! image link: http://www.ozone3d.net/blogs/lab/20120612/linux-mint-13-marco-et-les-terminaux-transparents/
        private MainViewModel MainVM { get; set; }
        private WebServer Server { get; set; }
        private Terminal Term { get; set; }

        public CommandPrompt(MainViewModel mv) {
            MainVM = mv;
            InitializeComponent();
            Term = Cons.Term;

            Loaded += (s, e) => {
                Term.AbortRequested += (ss, ee) => MessageBox.Show("Abort !");

                Term.RegisteredCommands.Add("set-root");
                Term.RegisteredCommands.Add("set-stat-fold");
                Term.RegisteredCommands.Add("load");
                Term.RegisteredCommands.Add("reload-all");
                Term.RegisteredCommands.Add("server");
                Term.RegisteredCommands.Add("get-root");
                Term.RegisteredCommands.Add("validate");
                Term.RegisteredCommands.Add("help");
                Term.RegisteredCommands.Add("exit");
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
            if (command.Name == "load" || command.Name == "reload-all") {
                MainVM.LoadStructureThreaded(SettingsManager.GetSetting("root") as string);
            } else if (command.Name == "set-root" && argLength == 1) {
                var root = new FolderNode(command.Args[0]);
                if (root.ValidRootFolder()) {
                    SettingsManager.SetSetting("root", command.Args[0]);
                } else {
                    Console.WriteLine("Cannot use {0} as a root folder.\nPlease choose another.", command.Args[0]);
                }
            } else if (command.Name == "set-stat-fold" && argLength == 1) {
                SettingsManager.SetSetting("statfolder", command.Args[0]);
            } else if (command.Name == "server") {
                var list = new List<string>();
                for (var i = 1; i < command.Args.Length; i++) {
                    list.Add(command.Args[i]);
                }
                Server.CheckCommands(command.Args[0], list.ToArray());
            } else if (command.Name == "exit") {
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
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time used: {0}", elapsedMs);
        }

        public void RunCommand(string command) {
            Term.RunCommand(command);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }
    }
}
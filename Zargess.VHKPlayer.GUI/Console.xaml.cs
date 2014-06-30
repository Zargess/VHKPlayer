using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zargess.VHKPlayer.GUI {
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console {
        public Console() {
            InitializeComponent();
            Term.Prompt = "\n> ";

            Loaded += (s, e) => {
                Term.CommandEntered += (ss, ee) => {
                    var msg = ee.Command.GetDescription("Command is '{0}'", " with args '{0}'", ", '{0}'", ".");
                    Term.Text += msg;
                    Term.InsertNewPrompt();
                };

                Term.AbortRequested += (ss, ee) => MessageBox.Show("Abort !");

                Term.RegisteredCommands.Add("hello");
                Term.RegisteredCommands.Add("world");
                Term.RegisteredCommands.Add("helloworld");
                Term.RegisteredCommands.Add("ls");
                Term.RegisteredCommands.Add("cd");
                Term.RegisteredCommands.Add("pwd");

                Term.Text += "Welcome !\n";
                Term.Text += "Hit tab to complete your current command.\n";
                Term.Text += "Use ctrl+c to raise an AbortRequested event.\n\n";
                Term.Text += "Available (fake) commands are:\n";
                Term.RegisteredCommands.ForEach(cmd => Term.Text += "  - " + cmd + "\n");
                Term.InsertNewPrompt();
            };
            int p;
            Term.InsertLineBeforePrompt("" + p);
        }
    }
}
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public object[] Items { get; set; }

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            Items = new object[] {
                new Category() {
                    Name = "main category",
                    Childs = new[] {
                        new Category() {
                            Name = "subcategory",
                            Content = "Test"
                        },
                        new Category() {
                            Name = "subcategory",
                            Content = "Test"
                        }
                    }
                },
                new Category() {
                    Name = "main category",
                    Childs = new[] {
                        new Category() {
                            Name = "subcategory",
                            Content = "Test"
                        },
                        new Category() {
                            Name = "subcategory",
                            Content = "Test"
                        }
                    }
                },
                new Category() {
                    Name = "main category",
                    Childs = new[] {
                        new Category() {
                            Name = "subcategory",
                            Content = "test"
                        },
                        new Category() {
                            Name = "subcategory",
                            Content = "test"
                        }
                    }
                },
                new Foo() {
                    Test = new object[] {
                        "hello",
                        "mate"
                    }
                }
            };
        }
    }

    public class Category {
        public object[] Childs { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class Foo {
        public object[] Test { get; set; }
    }
}
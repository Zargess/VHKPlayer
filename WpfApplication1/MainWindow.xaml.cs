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

        public Category[] Items { get; set; }

        public MainWindow() {


            InitializeComponent();
            DataContext = this;
            Items = new[] {
                new Category() {
                    Name = "main category",
                    Childs = new[] {
                        new Category() {Name = "subcategory"},
                        new Category() {Name = "subcategory"}
                    }
                },
                new Category() {
                    Name = "main category",
                    Childs = new[] {
                        new Category() {Name = "subcategory"},
                        new Category() {Name = "subcategory"}
                    }
                },
                new Category() {
                    Name = "main category",
                    Childs = new[] {
                        new Category() {Name = "subcategory"},
                        new Category() {Name = "subcategory"}
                    }
                },
                new Category() {
                    Name = "test",
                    Childs = new Category[0]
                }, 
            };
        }
    }

    public class Category {
        public Category[] Childs { get; set; }
        public string Name { get; set; }
    }
}

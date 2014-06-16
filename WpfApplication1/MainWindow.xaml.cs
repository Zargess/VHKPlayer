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

        public Category[] Categories { get; set; }

        public MainWindow() {


            InitializeComponent();

            Ild.DataContext = Categories = new[] {
                new Category() {
                    Name = "main category",
                    Categories = new[] {
                        new Category() {Name = "subcategory"},
                        new Category() {Name = "subcategory"}
                    }
                },
                new Category() {
                    Name = "main category",
                    Categories = new[] {
                        new Category() {Name = "subcategory"},
                        new Category() {Name = "subcategory"}
                    }
                },
                new Category() {
                    Name = "main category",
                    Categories = new[] {
                        new Category() {Name = "subcategory"},
                        new Category() {Name = "subcategory"}
                    }
                }
            };
        }
    }

    public class Category {
        public Category[] Categories { get; set; }
        public string Name { get; set; }
    }
}

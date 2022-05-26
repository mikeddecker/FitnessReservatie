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

namespace FitnessReservatieUI {
    /// <summary>
    /// Interaction logic for AdminOverzichtWindow.xaml
    /// </summary>
    public partial class AdminOverzichtWindow : Window {
        public AdminOverzichtWindow() {
            InitializeComponent();
        }

        private void ToestelToevoegenButton_Click(object sender, RoutedEventArgs e) {

        }

        private void ToestelVerwijderenButton_Click(object sender, RoutedEventArgs e) {

        }

        private void ToestelInOnderhoudButton_Click(object sender, RoutedEventArgs e) {

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}

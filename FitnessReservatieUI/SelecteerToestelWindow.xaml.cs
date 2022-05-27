using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Managers;
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
    /// Interaction logic for SelecteerToestelWindow.xaml
    /// </summary>
    public partial class SelecteerToestelWindow : Window {
        public Toestel geselecteerdeToestel;
        public SelecteerToestelWindow(List<Toestel> toestellen) {
            InitializeComponent();
            ToestelListBox.ItemsSource = toestellen;
        }

        private void SelecteerToestelButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void ToestelListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            geselecteerdeToestel = (Toestel)ToestelListBox.SelectedItem;
            SelecteerToestelButton.IsEnabled = true;
        }
    }
}

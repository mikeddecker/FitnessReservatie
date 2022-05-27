using FitnessReservatieBL.Managers;
using FitnessReservatieDL;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private ToestelManager toestelManager;
        public AdminOverzichtWindow() {
            InitializeComponent();
            toestelManager = new ToestelManager(new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()));
        }

        private void ToestelToevoegenButton_Click(object sender, RoutedEventArgs e) {
            ToestelToevoegenWindow toestelToevoegenWindow = new ToestelToevoegenWindow(toestelManager);
            toestelToevoegenWindow.ShowDialog();
        }

        private void ToestelVerwijderenButton_Click(object sender, RoutedEventArgs e) {
            ToestelVerwijderenWindow toestelVerwijderenWindow = new ToestelVerwijderenWindow(toestelManager);
            toestelVerwijderenWindow.ShowDialog();
        }

        private void ToestelInOnderhoudButton_Click(object sender, RoutedEventArgs e) {
            ToestelInOnderhoudZettenWindow toestelInOnderhoudZettenWindow = new ToestelInOnderhoudZettenWindow(toestelManager);
            toestelInOnderhoudZettenWindow.ShowDialog();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}

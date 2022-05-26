using FitnessReservatieBL.Domein;
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
    /// Interaction logic for ToestelToevoegenWindow.xaml
    /// </summary>
    public partial class ToestelToevoegenWindow : Window {
        private ToestelManager toestelManager;
        public ToestelToevoegenWindow() {
            InitializeComponent();
            toestelManager = new ToestelManager(new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()));
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Toestel toestel = toestelManager.VoegToestelToe(ToestelNaamTextBox.Text);
            MessageBox.Show(toestel.ToString());
            Close();
        }
    }
}

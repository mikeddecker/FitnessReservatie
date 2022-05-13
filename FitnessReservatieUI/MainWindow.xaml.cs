using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessReservatieUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private PersoonManager persoonManager;
        public MainWindow() {
            InitializeComponent();
            persoonManager = new PersoonManager(new PersoonRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) {

            try {
                string email = EmailTextBox.Text;
                Persoon persoon = persoonManager.LogPersoonIn(email);
                EmailControle.ControleerEmail(email);

                if (typeof(Klant) == persoon.GetType()) {
                    KlantReserveertSessieWindow klantReserveertSessieWindow = new KlantReserveertSessieWindow();
                    klantReserveertSessieWindow.ShowDialog();
                } else if (typeof(Admin) == persoon.GetType()) {
                    MessageBox.Show("Nog geen scherm voor Admin gemaakt");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    /// Interaction logic for KlantReserveertSessie.xaml
    /// </summary>
    public partial class KlantReserveertSessieWindow : Window {
        public KlantReserveertSessieWindow(Klant klant, FitnessManager fitnessManager) {
            InitializeComponent();
            this.klant = klant;
            ReservatieDatePicker.DisplayDateStart = DateTime.Today;
            ReservatieDatePicker.DisplayDateEnd = DateTime.Today.AddDays(7);
            TijdslotComboBox.ItemsSource = fitnessManager.GeefTijdsloten();
            toestellen = fitnessManager.GeefToestellen();
            ToestelComboBox.ItemsSource = new string[] { "eerst datum en tijd aub", "even geduld aub" };
            reservatieManager = new ReservatieManager(new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()));
        }

        private Klant klant;
        private ReservatieManager reservatieManager;
        private bool datumHasChanged = false;
        private bool tijdslotHasChanged = false;
        private IReadOnlyList<Toestel> toestellen;
        private void ReservatieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            datumHasChanged = true;
            UpdateToestellen();
        }
        private void UpdateToestellen() {
            if (datumHasChanged && tijdslotHasChanged) {
                ToestelComboBox.SelectedIndex++;

                ToestelComboBox.IsEnabled = true;
            }
        }

        private void TijdslotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tijdslotHasChanged = true;
            UpdateToestellen();
        }
    }
}

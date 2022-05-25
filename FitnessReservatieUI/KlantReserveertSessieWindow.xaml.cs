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
        public KlantReserveertSessieWindow(Klant klant) {
            InitializeComponent();
            //this.klant = klant;
            reservatieManager = new ReservatieManager(new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()), klant);


            // velden invullen en constricties opleggen.
            ReservatieDatePicker.DisplayDateStart = DateTime.Today;
            ReservatieDatePicker.DisplayDateEnd = DateTime.Today.AddDays(7);
            TijdslotComboBox.ItemsSource = reservatieManager.GeefTijdsloten();
            ToestelComboBox.ItemsSource = new string[] { "eerst datum en tijd aub", "even geduld aub" };

            // mogelijke toestellen preparen om te vergelijken met huidige reservaties.

            // reservaties ophalen klant voor controles, spreiden van load.
        }

        //private Klant klant;
        private ReservatieManager reservatieManager;
        private bool datumHasChanged = false;
        private bool tijdslotHasChanged = false;
        private Reservatie reservatie;
        private void ReservatieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            datumHasChanged = true;
            UpdateToestellen();
        }
        private void UpdateToestellen() {
            if (ToestelComboBox.IsEnabled) { // in het geval dat we na een toestel te kiezen nog een andere datum of tijdslot kiezen, 
                // controleren of het toestel nog mogelijk is.
                List<Toestel> toestellen = reservatieManager.GeefMogelijkeToestellen((DateTime)ReservatieDatePicker.SelectedDate, (Tijdslot)TijdslotComboBox.SelectedItem);
                Toestel geselecteerdeToestel = (Toestel)ToestelComboBox.SelectedItem;
                if (!toestellen.Contains(geselecteerdeToestel)) {
                    ToestelComboBox.ItemsSource = toestellen;
                }
            } else if (datumHasChanged && tijdslotHasChanged) {
                ToestelComboBox.ItemsSource = reservatieManager.GeefMogelijkeToestellen((DateTime)ReservatieDatePicker.SelectedDate, (Tijdslot)TijdslotComboBox.SelectedItem);
                ToestelComboBox.IsEnabled = true;
            }
        }

        private void TijdslotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tijdslotHasChanged = true;
            UpdateToestellen();

        }

        private void ToestelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            VoegToeButton.IsEnabled = true;
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e) {
            ReservatieDetail detail = new ReservatieDetail((DateTime)ReservatieDatePicker.SelectedDate, (Tijdslot)TijdslotComboBox.SelectedItem, (Toestel)ToestelComboBox.SelectedItem);
            reservatie.VoegReservatieDetailToe(detail);
            reservatieManager.MagKlantTijdslotReserveren(reservatie);
        }

        private void ReserveerButton_Click(object sender, RoutedEventArgs e) {
            reservatieManager.SchrijfReservatieInDB(reservatie);
            ReservatieDetailListBox.ItemsSource = "";
            ReservatieDatePicker.SelectedDate = null;
            ToestelComboBox.ItemsSource = new string[] { "eerst datum en tijd aub", "even geduld aub" };
            datumHasChanged = false;
            tijdslotHasChanged = false;
        }
    }
}

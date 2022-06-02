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
            reservatieManager = new ReservatieManager(new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()),
                new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()), klant);

            // velden invullen en constricties opleggen.
            ReservatieDatePicker.DisplayDateStart = DateTime.Today;
            ReservatieDatePicker.DisplayDateEnd = DateTime.Today.AddDays(7);
            TijdslotComboBox.ItemsSource = reservatieManager.GeefTijdsloten();
            UpdateReservatieDetailsListBox();
        }

        private ReservatieManager reservatieManager;
        private bool datumHasChanged = false;
        private bool tijdslotHasChanged = false;

        private void ReservatieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            // TODO if (today && tijdslotHasChanged) { controlleer tijdsloten, enkel reserveren vandaag NA dit uur }
            datumHasChanged = true;
            if (tijdslotHasChanged) {
                UpdateToestellen();
            }
        }
        private void UpdateToestellen() {
            if (ToestelComboBox.IsEnabled) { // in het geval dat we na een toestel te kiezen nog een andere datum of tijdslot kiezen, 
                // controleren of het toestel nog mogelijk is.
                Toestel geselecteerdeToestel = (Toestel)ToestelComboBox.SelectedItem;
                List<Toestel> toestellen = reservatieManager.GeefMogelijkeToestellen((DateTime)ReservatieDatePicker.SelectedDate, (Tijdslot)TijdslotComboBox.SelectedItem);
                ToestelComboBox.ItemsSource = toestellen;
                if (!toestellen.Contains(geselecteerdeToestel)) {
                    ToestelComboBox.SelectedItem = geselecteerdeToestel;
                }
            } else if (datumHasChanged && tijdslotHasChanged) {
                ToestelComboBox.ItemsSource = reservatieManager.GeefMogelijkeToestellen((DateTime)ReservatieDatePicker.SelectedDate, (Tijdslot)TijdslotComboBox.SelectedItem);
                ToestelComboBox.IsEnabled = true;
            }
        }

        private void TijdslotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tijdslotHasChanged = true;
            if (datumHasChanged) {
                UpdateToestellen();
            }
        }

        private void ToestelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // TODO if (datumHasChanged && datum today) { controlleer tijdsloten }
            VoegToeButton.IsEnabled = true;
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e) {
            ReservatieDetail detail = new ReservatieDetail((DateTime)ReservatieDatePicker.SelectedDate, (Tijdslot)TijdslotComboBox.SelectedItem, (Toestel)ToestelComboBox.SelectedItem);
            detail.ZetIsNieuw(true);
            if (reservatieManager.MagKlantTijdslotReserveren(detail)) {
                reservatieManager.VoegToeAanNieuweReservatie(detail);
                ResetDetails();
                UpdateReservatieDetailsListBox();
                ReserveerButton.IsEnabled = true;
                VoegToeButton.IsEnabled = false;
            } else {
                MessageBox.Show("Vergeet niet:\n " +
                    "--Je mag slechts 4 reservaties per dag hebben\n " +
                    "--Je mag een toestel maximaal 2 dagen na elkaar reserveren\n " +
                    "--Je kan geen 2 toestellen op hetzelfde moment reserveren.", "Reservatie niet toegelaten");
            }
        }
        private void UpdateReservatieDetailsListBox() {
            ReservatieDetailListBox.ItemsSource = reservatieManager.GeefReservatieDetailsVoorListBox();
        }
        private void ResetDetails() {
            datumHasChanged = false;
            tijdslotHasChanged = false;
            ReservatieDatePicker.SelectedDate = null;
            datumHasChanged = false;
            TijdslotComboBox.SelectedIndex = -1;
            tijdslotHasChanged = false;
            ToestelComboBox.ItemsSource = "";
        }

        private void ReserveerButton_Click(object sender, RoutedEventArgs e) {
            reservatieManager.SchrijfReservatieInDB();
            UpdateReservatieDetailsListBox();
            ResetDetails();
            ReserveerButton.IsEnabled = false;
        }
    }
}

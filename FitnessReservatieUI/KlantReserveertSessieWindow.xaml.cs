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
            this.klant = klant;
            reservatieManager = new ReservatieManager(new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FitnessReservatieDBConnection"].ToString()));


            // velden invullen en constricties opleggen.
            ReservatieDatePicker.DisplayDateStart = DateTime.Today;
            ReservatieDatePicker.DisplayDateEnd = DateTime.Today.AddDays(7);
            TijdslotComboBox.ItemsSource = reservatieManager.GeefTijdsloten();
            ToestelComboBox.ItemsSource = new string[] { "eerst datum en tijd aub", "even geduld aub" };

            // mogelijke toestellen preparen om te vergelijken met huidige reservaties.
            
            // reservaties ophalen klant voor controles, spreiden van load.
        }

        private Klant klant;
        private ReservatieManager reservatieManager;
        private bool datumHasChanged = false;
        private bool tijdslotHasChanged = false;
        private void ReservatieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            datumHasChanged = true;
            UpdateToestellen();
        }
        private void UpdateToestellen() {
            if (datumHasChanged && tijdslotHasChanged) {
                ToestelComboBox.SelectedIndex++;
                reservatieManager.GeefMogelijkeToestellen((DateTime)ReservatieDatePicker.SelectedDate, (Tijdslot)TijdslotComboBox.SelectedItem);
                ToestelComboBox.IsEnabled = true;
            }
        }

        private void TijdslotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tijdslotHasChanged = true;
            UpdateToestellen();
        }
    }
}

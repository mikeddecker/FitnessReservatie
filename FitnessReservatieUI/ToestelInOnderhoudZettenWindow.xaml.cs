using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
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
    /// Interaction logic for ToestelInOnderhoudZettenWindow.xaml
    /// </summary>
    public partial class ToestelInOnderhoudZettenWindow : Window {
        private ToestelManager toestelManager;
        private Toestel geselecteerdeToestel;
        public ToestelInOnderhoudZettenWindow(ToestelManager toestelManager) {
            InitializeComponent();
            this.toestelManager = toestelManager;
        }
        private void UpdateToestelButton_Click(object sender, RoutedEventArgs e) {
            // update knop is slechts enabled zijn als geselecteerdeToestel niet null is
            try {
                toestelManager.ZetToestelInOnderhoudDoorDefect(geselecteerdeToestel);
                // kijken of geselecteerde toestel al niet in onderhoud staat.
                Close();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Toestel updaten gefaald");
            }
        }
        private void ZoekButton_Click(object sender, RoutedEventArgs e) {
            try {
                // stap 1 toestel zoeken
                if (IDTextBox.Text.Length > 0) {
                    int id = int.Parse(IDTextBox.Text);
                    geselecteerdeToestel = toestelManager.GeefToestelMetID(id);
                } else { // nu is toestelnaam ingevuld
                    List<Toestel> eenOfMeerToestellen = toestelManager.GeefBeschikbareToestellen(ToestelnaamTextBox.Text);
                    if (eenOfMeerToestellen.Count() == 1) { // kan niet 0 zijn, dan gooit die een ToestelManagerException
                        geselecteerdeToestel = eenOfMeerToestellen[0];
                    } else {
                        SelecteerToestelWindow selecteerToestelWindow = new SelecteerToestelWindow(eenOfMeerToestellen);
                        selecteerToestelWindow.ShowDialog();
                        if (selecteerToestelWindow.DialogResult == true) {
                            geselecteerdeToestel = selecteerToestelWindow.geselecteerdeToestel;
                        }
                    }
                }
                if (geselecteerdeToestel is not null) {
                    // stap 2 toestelinfo tonen zodra een toestel gevonden
                    ToestelInfoLabel.Content = geselecteerdeToestel.ToString();

                    // stap 3 update knop beschikbaar maken zodra een toesel gevonden
                    UpdateButton.IsEnabled = true;
                }
            } catch (ToestelManagerException) {
                MessageBox.Show("Geen toestel gevonden");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void IDTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (IDTextBox.Text.Length > 0) {
                ToestelnaamTextBox.IsEnabled = false;
                ZoekButton.IsEnabled = true;
            } else {
                ToestelnaamTextBox.IsEnabled = true;
                ZoekButton.IsEnabled = false;
            }
        }
        private void ToestelnaamTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (ToestelnaamTextBox.Text.Length > 0) {
                IDTextBox.IsEnabled = false;
                ZoekButton.IsEnabled = true;
            } else {
                IDTextBox.IsEnabled = true;
                ZoekButton.IsEnabled = false;
            }
        }
    }
}

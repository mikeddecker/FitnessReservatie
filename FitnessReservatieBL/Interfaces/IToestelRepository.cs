using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IToestelRepository {
        List<Toestel> GeefMogelijkeToestellen(DateTime datum, Tijdslot tijdslot);
        void VerwijderToestel(int toestelID);
        void UpdateToestelBeschikbaarheid(int toestelID, bool beschikbaar);
        bool HeeftToestelToekomstigeReservaties(int toestelID);
        int SchrijfToestelInDB(Toestel nieuwToestel);
        Dictionary<int, Toestel> GeefBeschikbareToestellen();
        List<int> GeefToestelIDsZonderOpenstaandeReservaties(string zoektekst);
    }
}

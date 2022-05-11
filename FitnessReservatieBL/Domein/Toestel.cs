using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Toestel {
        public Toestel(string toestelnaam, bool beschikbaar) {
            ZetType(toestelnaam);
            ZetBeschikbaarheid(beschikbaar);
            //ZetAfbeeldingUrl(afbeeldingUrl);
        }

        public int ToestelID { get; private set; }
        public string Type { get; private set; }
        public bool Beschikbaar { get; private set; }
        //public string AfbeeldingUrl { get; private set; }
        public void ZetId(int id) {
            if (id <= 0) { throw new ToestelException("ZetId - id moet groter zijn dan 0"); }
        }
        public void ZetType(string toestelnaam) {
            if (string.IsNullOrWhiteSpace(toestelnaam)) { throw new ToestelException("ZetVoornaam"); }
            Type = toestelnaam.Trim().ToLower();
        }
        public void ZetBeschikbaarheid(bool beschikbaar) {
            //if (beschikbaar == Beschikbaar) { throw new ToestelException($"ZetDefect - het toestel {ToestelID} had deze status al");  } // --> niet persé nodig
            Beschikbaar = beschikbaar;
        }

        public override bool Equals(object obj) { // toestel is hetzelfde op basis van ID
            return obj is Toestel toestel &&
                   ToestelID == toestel.ToestelID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ToestelID);
        }
    }
}

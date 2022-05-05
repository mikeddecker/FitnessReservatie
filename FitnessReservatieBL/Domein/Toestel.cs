using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Toestel {
        public Toestel(string toestelnaam, string afbeeldingUrl) {
            ZetToestelnaam(toestelnaam);
            ZetAfbeeldingUrl(afbeeldingUrl);
        }

        public string Toestelnaam { get; private set; }
        public string AfbeeldingUrl { get; private set; }
        public void ZetToestelnaam(string toestelnaam) {
            if (string.IsNullOrWhiteSpace(toestelnaam)) { throw new ToestelException("ZetVoornaam"); }
            Toestelnaam = toestelnaam.Trim();
        }
        public void ZetAfbeeldingUrl(string url) {
            if (string.IsNullOrWhiteSpace(url)) { throw new ToestelException("ZetVoornaam"); }
            AfbeeldingUrl = url.Trim();
        }
    }
}

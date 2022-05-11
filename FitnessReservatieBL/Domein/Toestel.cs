using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Toestel {
        public Toestel(string toestelnaam, string afbeeldingUrl) {
            ZetType(toestelnaam);
            //ZetAfbeeldingUrl(afbeeldingUrl);
        }

        public int ToestelID { get; private set; }
        public string Type { get; private set; }
        public bool Defect { get; private set; }
        //public string AfbeeldingUrl { get; private set; }
        public void ZetId(int id) {
            if (id < 1) { throw new ToestelException("ZetId - id mag niet kleiner zijn dan 1"); }
        }
        public void ZetType(string toestelnaam) {
            if (string.IsNullOrWhiteSpace(toestelnaam)) { throw new ToestelException("ZetVoornaam"); }
            Type = toestelnaam.Trim();
        }
        public void ZetDefect(bool defect) {
            if (defect == Defect) { throw new ToestelException($"ZetDefect - het toestel {ToestelID} had deze status al");  }
            Defect = defect;
        }
        //public void ZetAfbeeldingUrl(string url) {
        //    if (string.IsNullOrWhiteSpace(url)) { throw new ToestelException("ZetVoornaam"); }
        //    AfbeeldingUrl = url.Trim();
        //}
    }
}

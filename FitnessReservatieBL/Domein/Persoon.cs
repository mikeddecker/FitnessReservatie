using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public abstract class Persoon {
        protected Persoon(int iD, string voornaam, string achternaam, string email) {
            ZetID(iD);
            ZetVoornaam(voornaam);
            ZetAchternaam(achternaam);
            ZetEmail(email);
        }

        public int ID { get; private set; }
        public string Voornaam { get; private set; }
        public string Achternaam { get; private set; }
        public string Email { get; private set; }


        public void ZetID(int id) {
            if (id <= 0) { throw new KlantException("ZetID - id moet groeter zijn dan 0"); }
            ID = id;
        }
        public void ZetVoornaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) { throw new KlantException("ZetVoornaam"); }
            Voornaam = naam.Trim();
        }
        public void ZetAchternaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) { throw new KlantException("ZetAchternaam"); }
            Achternaam = naam.Trim();
        }
        public void ZetEmail(string email) {
            //TODO emailcontrole opsplitsen naar EmailControle klasse + unit test aanpassen
            if (string.IsNullOrWhiteSpace(email)) { throw new KlantException("ZetEmail - null or white space"); }
            try {
                EmailControle.ControleerEmail(email);
            } catch (Exception ex) {
                throw new KlantException("ZetEmail - fout in de email", ex);
            }
            Email = email.Trim().ToLower();
        }
    }
}

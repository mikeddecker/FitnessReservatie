using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Klant {
        internal Klant(int iD, string voornaam, string achternaam, string email) {
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
            if (string.IsNullOrWhiteSpace(email)) { throw new KlantException("ZetEmail - null or white space"); }
            if (!email.Contains("@")) { throw new KlantException("ZetEmail - Email bevat geen @"); }
            if (email.StartsWith("@")) { throw new KlantException("ZetEmail - Email start met @"); }
            if (email.EndsWith("@")) { throw new KlantException("ZetEmail - Email eindigt met @"); }
            if (!email.Substring(email.IndexOf("@")).Contains(".")) { throw new KlantException("ZetEmail - Email bevat geen correct domein"); }
            if (email.Substring(email.IndexOf("@") + 1, 1).Contains(".")) { throw new KlantException("ZetEmail - Email bevat geen domein zoals .be of ..."); }
            Email = email.Trim().ToLower();
        }
    }
}

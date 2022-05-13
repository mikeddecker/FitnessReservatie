using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Reservatie {
        public int ReservatieID { get; private set; }
        public Klant Klant { get; private set; }
        public List<ReservatieDetail> ReservatieDetails { get; init; }
        public void ZetReservatieID(int id) {
            if (id <= 0) { throw new ReservatieException("ZetReservatieID - id moet groter zijn dan 0"); }
            ReservatieID = id;
        }
        public void ZetKlant(Klant klant) {
            if (klant == null) { throw new ReservatieException("ZetKlant - Klant is null"); }
            if (klant.ID == 0) { throw new ReservatieException("ZetKlant - KlantID is niet ingesteld"); }
            if (string.IsNullOrWhiteSpace(klant.Voornaam)) { throw new ReservatieException("ZetKlant - Voornaam klant is ongeldig"); }
            if (string.IsNullOrWhiteSpace(klant.Achternaam)) { throw new ReservatieException("ZetKlant - Achternaam klant is ongeldig"); }
            try {
                EmailControle.ControleerEmail(klant.Email);
            } catch (EmailControleException) {
                throw new ReservatieException("ZetKlant - klant heeft een ongeldige email");
            }

            Klant = klant;
        }
        public void VoegReservatieDetailToe(ReservatieDetail detail) {
            if (detail == null) { throw new ReservatieException("VoegReservatieDetailToe - ReservatieDetails mogen niet leeg zijn"); }
            if (ReservatieDetails.Contains(detail)) { throw new ReservatieDetailException("VoegReservatieDetailToe - details zaten al in de reservatie"); }
            ReservatieDetails.Add(detail);
        }
    }
}

using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Reservatie {
        public Reservatie(Klant klant) {
            ZetKlant(klant);
            ReservatieDetails = new List<ReservatieDetail>();
        }

        public int ReservatieID { get; private set; }
        public Klant Klant { get; private set; }
        public List<ReservatieDetail> ReservatieDetails { get; init; }
        public void ZetReservatieID(int id) {
            if (id <= 0) { throw new ReservatieException("ZetReservatieID - id moet groter zijn dan 0"); }
            ReservatieID = id;
        }
        public void ZetKlant(Klant klant) {
            if (klant == null) { throw new ReservatieException("ZetKlant - Klant is null"); }
            Klant = klant;
        }

        public void VoegReservatieDetailToe(ReservatieDetail detail) {
            if (detail == null) { throw new ReservatieException("VoegReservatieDetailToe - ReservatieDetails mogen niet leeg zijn"); }
            if (ReservatieDetails.Contains(detail)) { throw new ReservatieDetailException("VoegReservatieDetailToe - details zaten al in de reservatie"); }
            ReservatieDetails.Add(detail);
        }

        public override bool Equals(object obj) {
            return obj is Reservatie reservatie &&
                   ReservatieID == reservatie.ReservatieID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ReservatieID);
        }
    }
}

using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Reservatie {
        public int ReservatieID { get; private set; }
        public int Klantnummer { get; private set; }
        public List<ReservatieDetail> ReservatieDetails { get; init; }
        public void ZetReservatieID(int id) {
            if (id <= 0) { throw new ReservatieException("ZetReservatieID - id moet groter zijn dan 0"); }
            ReservatieID = id;
        }
        public void ZetKlantnummer(int klantnr) {
            if (klantnr <= 0) { throw new ReservatieException("ZetKlantnummer - id van de klant moet groeter zijn dan 0"); }
            Klantnummer = klantnr;
        }
        public void VoegReservatieDetailToe(ReservatieDetail detail) {
            if (detail == null) { throw new ReservatieException("VoegReservatieDetailToe - ReservatieDetails mogen niet leeg zijn"); }
            if (ReservatieDetails.Contains(detail)) { throw new ReservatieDetailException("VoegReservatieDetailToe - details zaten al in de reservatie"); }
            ReservatieDetails.Add(detail);
        }
    }
}

using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class ReservatieDetail {
        public ReservatieDetail(DateTime datum, Tijdslot tijdslot, Toestel toestel) {
            ZetDatum(datum);
            ZetTijdslot(tijdslot);
            ZetToestel(toestel);
            IsNieuw = false;
        }

        public DateTime Datum { get; private set; }
        public Tijdslot Tijdslot { get; private set; }
        public Toestel Toestel { get; private set; }
        public bool IsNieuw { get; private set; }

        //public DateTime beginuur = new DateTime((long)10000 * 1000 * 60 * 60 * 8);

        public void ZetDatum(DateTime datum) {
            if (datum < DateTime.Today) { throw new ReservatieDetailException("ZetDatum - Een reservatie in het verleden kan niet"); }
            if (datum > DateTime.Today.AddDays(7)) { throw new ReservatieDetailException("ZetDatum - Een reservatie kan niet langer dan 7 dagen op voorhand gereserveerd worden"); }
            Datum = datum;
        }
        public void ZetToestel(Toestel t) {
            if (t == null) { throw new ReservatieDetailException("ZetToestel - toestel is null"); }
            // onderstaande wordt afgedwongen door de reservatieManager (want als ik inlaad, kan een toestel momenteel nog wel onbeschikbaar zijn)
            //if (!t.Beschikbaar) { throw new ReservatieDetailException("ZetToestel - toestel is niet beschikbaar"); }
            Toestel = t;
        }
        public void ZetTijdslot(Tijdslot tijdslot) {
            if (tijdslot == null) { throw new ReservatieDetailException("ZetTijdslot - geen tijdslot"); }
            Tijdslot = tijdslot;
        }

        public void ZetIsNieuw(bool isNieuw) {
            IsNieuw = isNieuw;
        }

        public override bool Equals(object obj) {
            return obj is ReservatieDetail detail &&
                   Datum.ToShortDateString() == detail.Datum.ToShortDateString() &&
                   Tijdslot.Equals(detail.Tijdslot) &&
                   Toestel.Equals(detail.Toestel);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Datum, Tijdslot, Toestel);
        }

        public override string ToString() {
            string details = $"{Datum.ToShortDateString()} : {Tijdslot.Beginuur}-{Tijdslot.Einduur} {Toestel.Type} {Toestel.ToestelID}";
            if (IsNieuw) { details += " (nieuw)"; }
            return details;
        }
    }
}

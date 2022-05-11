using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class ReservatieDetail {
        public DateTime Datum { get; private set; }
        public int Tijdslot { get; private set; }
        public Toestel Toestel { get; private set; }

        //public DateTime beginuur = new DateTime((long)10000 * 1000 * 60 * 60 * 8);

        public void ZetDatum(DateTime datum) {
            if (datum < DateTime.Today) { throw new ReservatieDetailException("ZetDatum - Een reservatie in het verleden kan niet"); }
            if (datum > DateTime.Today.AddDays(7)) { throw new ReservatieDetailException("ZetDatum - Een reservatie kan niet langer dan 7 dagen op voorhand gereserveerd worden"); }
            Datum = datum;
        }
        public void ZetToestel(Toestel t) {
            if (t == null) { throw new ReservatieDetailException("ZetToestel - toestel is null"); }
            if (!t.Beschikbaar) { throw new ReservatieDetailException("ZetToestel - toestel is niet beschikbaar"); }
            Toestel = t;
        }
        public void ZetTijdslot(int tijdslotID) { 
            if (tijdslotID <= 0) { throw new ReservatieDetailException("ZetTijdslot - Ongeldige tijdslotID"); }
            Tijdslot = tijdslotID;
        }
    }
}

using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Tijdslot {

        public Tijdslot(int id, TimeSpan beginuur, TimeSpan einduur) {
            ZetID(id);
            ZetBeginuur(beginuur);
            ZetEinduur(einduur);
        }
        public int TijdslotID { get; private set; } //id van uit de databank
        public TimeSpan Beginuur { get; private set; }
        public TimeSpan Einduur { get; private set; }

        public void ZetID(int id) {
            if (id <= 0) { throw new TijdslotException("ZetID - ongeldige ID"); }
            TijdslotID = id;
        }
        public void ZetBeginuur(TimeSpan beginuur) {
            if (beginuur.TotalHours > 24) { throw new TijdslotException("ZetBeginuur - ongeldig beginuur"); }
            if (Einduur != default(TimeSpan) && Einduur < beginuur) { throw new TijdslotException("ZetBeginuur - beginuur is groter dan einduur"); } 
            Beginuur = beginuur;
        }
        public void ZetEinduur(TimeSpan einduur) {
            if (einduur.TotalHours > 24) { throw new TijdslotException("ZetBeginuur - ongeldig einduur"); }
            if (einduur <= Beginuur) { throw new TijdslotException("ZetEinduur - Einduur kan niet voor beginuur zijn"); }
            Einduur = einduur;
        }

        public override bool Equals(object obj) { // Tijdslot met hetzelfde ID is al hetzelfde
            return obj is Tijdslot tijdslot &&
                   TijdslotID == tijdslot.TijdslotID;
        }
        public override int GetHashCode() {
            return HashCode.Combine(TijdslotID);
        }
        public override string ToString() {
            return $"{Beginuur.Hours}u tot {Einduur.Hours}u";
        }
    }
}

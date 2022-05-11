using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Tijdslot {
        public int TijdslotID { get; private set; } //id van uit de databank
        public TimeSpan Beginuur { get; private set; }
        public TimeSpan Einduur { get; private set; }

        public void ZetID(int id) {
            if (id <= 0) { throw new TijdslotException("ZetID - ongeldige ID"); }
            TijdslotID = id;
        }
        public void ZetBeginUur(TimeSpan beginuur) {
            if (beginuur.TotalHours > 24) { throw new TijdslotException("ZetBeginuur - ongeldig beginuur"); }
            Beginuur = beginuur;
        }
        public void ZetEinduur(TimeSpan einduur) {
            if (einduur.TotalHours > 24) { throw new TijdslotException("ZetBeginuur - ongeldig einduur"); }
            if (einduur <= Beginuur) { throw new TijdslotException("ZetEinduur - Einduur kan niet voor beginuur zijn"); }
            Einduur = einduur;
        }
    }
}

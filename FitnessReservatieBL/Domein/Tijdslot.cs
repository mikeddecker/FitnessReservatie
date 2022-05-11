using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Tijdslot {
        public int TijdslotID { get; private set; }
        public TimeSpan BeginUur { get; private set; }
        public TimeSpan EindUur { get; private set; }
    }
}

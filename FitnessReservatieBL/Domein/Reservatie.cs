using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Reservatie {
        public int ReservatieID { get; private set; }
        public int Klantnummer { get; private set; }
        public string Email { get; private set; }
        public string Voornaam { get; private set; }
        public string Achternaam { get; private set; }
        public List<ReservatieDetail> ReservatieDetails { get; init; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Admin : Persoon {
        internal Admin(int iD, string voornaam, string achternaam, string email) : base(iD, voornaam, achternaam, email) {
        }
    }
}

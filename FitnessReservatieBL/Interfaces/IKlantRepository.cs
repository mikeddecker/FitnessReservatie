using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IKlantRepository {
        bool BestaatKlant(string email);
        Persoon SelecteerKlant(string email);
    }
}

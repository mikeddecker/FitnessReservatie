using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IPersoonRepository {
        bool BestaatPersoon(string email, int? id);
        Persoon SelecteerPersoon(string email, int? id);
    }
}

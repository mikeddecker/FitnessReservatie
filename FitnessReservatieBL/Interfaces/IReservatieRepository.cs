using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IReservatieRepository {
        List<Reservatie> GeefReservaties();
        Reservatie SchrijfReservatieInDB(Reservatie reservatie);
    }
}

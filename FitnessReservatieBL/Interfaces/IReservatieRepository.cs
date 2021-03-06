using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IReservatieRepository {
        List<Tijdslot> GeefTijdsloten();
        void SchrijfReservatieInDB(Reservatie reservatie);
        List<ReservatieDetail> GeefToekomstigeReservatieDetais(int klantnummer);
    }
}

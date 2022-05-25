using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IReservatieRepository {
        List<Tijdslot> GeefTijdsloten();
        //List<Toestel> GeefToestellen();
        List<Toestel> GeefMogelijkeToestellen(DateTime datum, Tijdslot tijdslot);
        Reservatie SchrijfReservatieInDB(Reservatie reservatie);
    }
}

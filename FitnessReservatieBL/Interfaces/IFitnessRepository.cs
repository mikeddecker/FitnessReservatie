using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IFitnessRepository {
        IReadOnlyList<Tijdslot> GeefTijdsloten();
        IReadOnlyList<Toestel> GeefToestellen();
    }
}

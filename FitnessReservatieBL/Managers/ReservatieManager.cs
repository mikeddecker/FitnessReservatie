using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class ReservatieManager {
        private IReservatieRepository reservatieRepo;
        private IReadOnlyList<Tijdslot> tijdsloten;

        public ReservatieManager(IReservatieRepository repository) {
            reservatieRepo = repository;
            tijdsloten = reservatieRepo.GeefTijdsloten();
        }
        public List<Tijdslot> GeefTijdsloten() {
            return reservatieRepo.GeefTijdsloten();
        }
        public List<Toestel> GeefMogelijkeToestellen(DateTime datum, Tijdslot tijdslot) {
            return reservatieRepo.GeefMogelijkeToestellen(datum, tijdslot);
        }
        public Reservatie SchrijfReservatieInDB(Reservatie reservatie) {
            if (reservatie == null) { throw new ReservatieManagerException("SchrijfReservatieInDB - reservatie is null"); }
            //if (reservatie.Klant.ID <= 0) { throw new ReservatieManagerException("SchrijfReservatieInDB - klant ID is niet ingevuld"); }
            if (reservatie.ReservatieDetails.Count() == 0) { throw new ReservatieManagerException("SchrijfReservatieInDB - Geen reservatiedetails ingevuld"); }
            return reservatieRepo.SchrijfReservatieInDB(reservatie);
        }

    }
}

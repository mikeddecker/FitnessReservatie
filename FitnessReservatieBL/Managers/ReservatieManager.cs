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
        private Klant klant;

        public ReservatieManager(IReservatieRepository repository, Klant klant) {
            reservatieRepo = repository;
            tijdsloten = reservatieRepo.GeefTijdsloten();
            this.klant = klant;
        }
        public List<Tijdslot> GeefTijdsloten() {
            return reservatieRepo.GeefTijdsloten();
        }
        public List<Toestel> GeefMogelijkeToestellen(DateTime datum, Tijdslot tijdslot) {
            //TODO controlleren met klant of er geen 2 toestellen na elkaar zijn en niet meer dan 4 reservaties op een dag
            return reservatieRepo.GeefMogelijkeToestellen(datum, tijdslot);
        }
        public bool MagKlantTijdslotReserveren(Reservatie reservatie) {
            var r = klant.ToekomstieReservaties.SelectMany(x => x.ReservatieDetails.ToList());
            return false;
        }
        public Reservatie SchrijfReservatieInDB(Reservatie reservatie) {
            if (reservatie == null) { throw new ReservatieManagerException("SchrijfReservatieInDB - reservatie is null"); }
            //if (reservatie.Klant.ID <= 0) { throw new ReservatieManagerException("SchrijfReservatieInDB - klant ID is niet ingevuld"); }
            if (reservatie.ReservatieDetails.Count() == 0) { throw new ReservatieManagerException("SchrijfReservatieInDB - Geen reservatiedetails ingevuld"); }
            return reservatieRepo.SchrijfReservatieInDB(reservatie);
        }

    }
}

using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class FitnessManager {
        private IFitnessRepository fitnessRepository;

        private IReadOnlyList<Tijdslot> tijdsloten;
        private IReadOnlyList<Toestel> toestellen;
        public FitnessManager(IFitnessRepository reservatieRepository) {
            this.fitnessRepository = reservatieRepository;
            tijdsloten = reservatieRepository.GeefTijdsloten();
            toestellen = reservatieRepository.GeefToestellen();
        }
        public IReadOnlyList<Tijdslot> GeefTijdsloten() {
            return tijdsloten;
        }
        public IReadOnlyList<Toestel> GeefToestellen() {
            return toestellen;
        }
    }
}

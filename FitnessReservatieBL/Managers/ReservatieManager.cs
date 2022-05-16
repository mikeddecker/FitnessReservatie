using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class ReservatieManager {
        private IFitnessRepository Repository;

        public ReservatieManager(IFitnessRepository repository) {
            Repository = repository;
        }


    }
}

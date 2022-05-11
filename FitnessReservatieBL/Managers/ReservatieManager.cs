using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class ReservatieManager {
        private IReservatieRepository Repository;

        public ReservatieManager(IReservatieRepository repository) {
            Repository = repository;
        }


    }
}

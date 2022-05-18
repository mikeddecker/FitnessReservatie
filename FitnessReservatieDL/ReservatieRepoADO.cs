using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL {
    public class ReservatieRepoADO : IReservatieRepository {
        private string connectieString;
        public ReservatieRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
    }
}

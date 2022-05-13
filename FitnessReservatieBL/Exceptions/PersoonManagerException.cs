using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class PersoonManagerException : Exception {
        public PersoonManagerException(string message) : base(message) {
        }

        public PersoonManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class KlantManagerException : Exception {
        public KlantManagerException(string message) : base(message) {
        }

        public KlantManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

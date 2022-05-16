using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class FitnessManagerException : Exception {
        public FitnessManagerException(string message) : base(message) {
        }

        public FitnessManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

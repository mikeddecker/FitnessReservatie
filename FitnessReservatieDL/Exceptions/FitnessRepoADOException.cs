using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.Exceptions {
    public class FitnessRepoADOException : Exception {
        public FitnessRepoADOException(string message) : base(message) {
        }

        public FitnessRepoADOException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

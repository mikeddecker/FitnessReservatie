using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.Exceptions {
    public class ReservatieRepoADOException : Exception {
        public ReservatieRepoADOException(string message) : base(message) {
        }

        public ReservatieRepoADOException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

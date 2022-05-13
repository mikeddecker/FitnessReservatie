using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.Exceptions {
    public class PersoonRepoADOException : Exception {
        public PersoonRepoADOException(string message) : base(message) {
        }

        public PersoonRepoADOException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.Exceptions {
    public class ToestelRepoADOException : Exception {
        public ToestelRepoADOException(string message) : base(message) {
        }

        public ToestelRepoADOException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class ToestelManagerException : Exception {
        public ToestelManagerException(string message) : base(message) {
        }

        public ToestelManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

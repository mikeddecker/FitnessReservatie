using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class ToestelException : Exception {
        public ToestelException(string message) : base(message) {
        }

        public ToestelException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

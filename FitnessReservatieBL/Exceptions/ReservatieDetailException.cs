using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class ReservatieDetailException : Exception {
        public ReservatieDetailException(string message) : base(message) {
        }

        public ReservatieDetailException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

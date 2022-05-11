using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class TijdslotException : Exception {
        public TijdslotException(string message) : base(message) {
        }

        public TijdslotException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

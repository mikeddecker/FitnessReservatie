using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions {
    public class EmailControleException : Exception {
        public EmailControleException(string message) : base(message) {
        }

        public EmailControleException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

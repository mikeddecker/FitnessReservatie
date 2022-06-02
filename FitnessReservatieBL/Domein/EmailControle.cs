using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class EmailControle {
        public static bool ControleerEmail(string email) {
            email = email.ToLower();
            if (string.IsNullOrWhiteSpace(email)) { throw new EmailControleException("ZetEmail - null or white space"); }
            if (!email.Contains("@")) { throw new EmailControleException("ZetEmail - Email bevat geen @"); }
            if (email.StartsWith("@")) { throw new EmailControleException("ZetEmail - Email start met @"); }
            if (email.EndsWith("@")) { throw new EmailControleException("ZetEmail - Email eindigt met @"); }
            if (!email.Substring(email.IndexOf("@")).Contains(".")) { throw new EmailControleException("ZetEmail - Email bevat geen correct domein"); }
            if (email.Substring(email.IndexOf("@") + 1, 1).Contains(".")) { throw new EmailControleException("ZetEmail - Email bevat geen domein zoals .be of ..."); }
            return true;
        }
    }
}

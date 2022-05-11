using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class KlantManager {
        private IKlantRepository Repository;

        public KlantManager(IKlantRepository repository) {
            Repository = repository;
        }

        public Persoon LogKlantIn(string email) {
            try {
                if (Repository.BestaatKlant(email)) {
                    return Repository.SelecteerKlant(email);
                } else {
                    throw new KlantManagerException("LogKlantIn - klant niet gevonden");
                }
            } catch (KlantManagerException) {
                throw;
            } catch (Exception ex) {
                throw new KlantManagerException("LogKlantIn", ex);
            }
        }
    }
}

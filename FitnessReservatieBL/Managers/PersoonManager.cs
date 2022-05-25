using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class PersoonManager {
        private IPersoonRepository Repository;

        public PersoonManager(IPersoonRepository repository) {
            Repository = repository;
        }

        public Persoon LogPersoonIn(string email) {
            try {
                // Eerst email controle, gooit exception als het emailadres verkeerd is, geeft true als het een goed email adres is.
                EmailControle.ControleerEmail(email);

                //Nu is de mail toch al enigszins een deftige format.
                if (Repository.BestaatPersoon(email)) {
                    return Repository.SelecteerPersoon(email, null);
                } else {
                    throw new PersoonManagerException("LogPersoonIn - Wij kennen dit emailadres niet.");
                }
            } catch (EmailControleException) {
                throw;
            } catch (PersoonManagerException) {
                throw;
            } catch (Exception ex) {
                throw new PersoonManagerException("LogPersoonIn", ex);
            }
        }
    }
}   

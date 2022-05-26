using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class ToestelManager {
        private IToestelRepository toestelRepo;
        private Dictionary<int, Toestel> toestellen; /* toestelID, toestel : die gelijdelijk aan gevuld wordt, ofwel in een keer gevuld.
                                                      * Bij reservaties, worden enkel de nodige toestellen ingeladen */
        

        public ToestelManager(IToestelRepository toestelRepo) {
            this.toestelRepo = toestelRepo;
        }
        //TODO try catch toevoegen bij reservatieManager
        public Toestel VoegToestelToe(string type) {
            try {
                Toestel nieuwToestel = new Toestel(type, true);
                int toestelID = toestelRepo.SchrijfToestelInDB(nieuwToestel);
                nieuwToestel.ZetId(toestelID);
                toestellen.Add(toestelID, nieuwToestel);
                return nieuwToestel;
            } catch (Exception ex) {
                throw new ToestelManagerException("VoegToestelToe", ex);
            }
        }
        public bool HeeftToestelToekomstigeReservaties(int toestelID) {
            try {
                return toestelRepo.HeeftToestelToekomstigeReservaties(toestelID);
            } catch (Exception ex) {
                throw new ToestelManagerException("HeeftToestelToekomstigeReservaties", ex);
            }
        }
        public void ZetToestelInOnderhoud(Toestel toestel) {
            try {
                toestelRepo.UpdateToestelBeschikbaarheid(toestel.ToestelID, false);
                toestel.ZetBeschikbaarheid(false);
            } catch (Exception ex) {
                throw new ToestelManagerException("ZetToestelInOnderhoud", ex);
            }
        }
        public void VerwijderToestel(Toestel toestel) {
            try {
                toestelRepo.VerwijderToestel(toestel.ToestelID);
                toestel.ZetBeschikbaarheid(false);
            } catch (Exception ex) {
                throw new ToestelManagerException("VerwijderToestel", ex);
            }
        }
    }
}

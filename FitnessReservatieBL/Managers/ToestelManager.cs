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
        //TODO try catch toevoegen bij reservatieManager
        private IToestelRepository toestelRepo;
        private Dictionary<int, Toestel> toestellen = new Dictionary<int, Toestel>(); /* toestelID, toestel : die gelijdelijk aan gevuld wordt, ofwel in een keer gevuld.
                                                      * Adminzaken, alles
                                                      * Bij reservaties, worden enkel de nodige toestellen ingeladen
                                                      */
        public ToestelManager(IToestelRepository toestelRepo) {
            this.toestelRepo = toestelRepo;
            toestellen = GeefBeschikbareToestellen();
        }
        private Dictionary<int, Toestel> GeefBeschikbareToestellen() {
            return toestelRepo.GeefBeschikbareToestellen();
        }
        public List<Toestel> GeefBeschikbareToestellen(string zoektekst) {
            List<Toestel> gevondenToestellen = toestellen.Where(t => t.Value.Beschikbaar == true && t.Value.Type == zoektekst).Select(t => t.Value).ToList(); ;
            if (gevondenToestellen.Count() > 0) {
                return gevondenToestellen;
            } else {
                throw new ToestelManagerException($"GeefBeschikbareToestellen - \"{zoektekst}\" niet gevonden");
            }
        }
        
        public Toestel GeefToestelMetID(int id) {
            if (toestellen.ContainsKey(id) && toestellen[id].Beschikbaar) {
                return toestellen[id];
            } else {
                throw new ToestelManagerException("GeefToestelMetID - dit ID kennen we niet");
            }
        }
        public List<Toestel> GeefToestellenZonderOpenstaandeReservaties(string zoektekst) {
            List<int> gevondenToestelIDs = toestelRepo.GeefToestelIDsZonderOpenstaandeReservaties(zoektekst);
            if (gevondenToestelIDs.Count() > 0) {
                return toestellen.Where(kvp => gevondenToestelIDs.Contains(kvp.Value.ToestelID)).Select(kvp => kvp.Value).ToList();
            } else {
                throw new ToestelManagerException($"GeefBeschikbareToestellen - \"{zoektekst}\" niet gevonden");
            }
        }
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
        public void ZetToestelInOnderhoudDoorDefect(Toestel toestel) {
            try {
                toestelRepo.UpdateToestelBeschikbaarheid(toestel.ToestelID, false);
                toestel.ZetBeschikbaarheid(false);
            } catch (Exception ex) {
                throw new ToestelManagerException("ZetToestelInOnderhoudDoorDefect", ex);
            }
        }
        public void VerwijderToestel(Toestel toestel) {
            try {
                toestelRepo.VerwijderToestel(toestel.ToestelID);
                toestellen.Remove(toestel.ToestelID);
            } catch (Exception ex) {
                throw new ToestelManagerException("VerwijderToestel", ex);
            }
        }
    }
}

using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers {
    public class ReservatieManager {
        private IReservatieRepository reservatieRepo;
        private IReadOnlyList<Tijdslot> tijdsloten;
        private Klant klant;
        private List<ReservatieDetail> toekomstigeReservatiesKlant;
        private List<ReservatieDetail> nieuweReservatiesKlant;

        public ReservatieManager(IReservatieRepository repository, Klant klant) {
            reservatieRepo = repository;
            this.klant = klant;
            tijdsloten = reservatieRepo.GeefTijdsloten();
            toekomstigeReservatiesKlant = reservatieRepo.GeefToekomstigeReservatieDetais(klant.ID);
            nieuweReservatiesKlant = new List<ReservatieDetail>();
        }
        public IReadOnlyList<Tijdslot> GeefTijdsloten() {
            //TODO filteren zodat vandaag enkel uren geeft na dit uur
            return tijdsloten;
        }
        public List<Toestel> GeefMogelijkeToestellen(DateTime datum, Tijdslot tijdslot) {
            //TODO controlleren met klant of er geen 2 toestellen na elkaar zijn en niet meer dan 4 reservaties op een dag
            return reservatieRepo.GeefMogelijkeToestellen(datum, tijdslot);
        }
        public bool MagKlantTijdslotReserveren(ReservatieDetail detail) {
            List<ReservatieDetail> alleDetails = new List<ReservatieDetail>();
            foreach (ReservatieDetail det in toekomstigeReservatiesKlant) { alleDetails.Add(det); }
            foreach (ReservatieDetail det in nieuweReservatiesKlant) { alleDetails.Add(det); }
            if (IsVrijTijdslotVoorKlant(alleDetails, detail) && MinderDan4ReservatiesOpDag(alleDetails, detail) && Geen3TijdslotenEenzelfdeToestelNaElkaar(alleDetails, detail)) {
                return true;
            }
            return false;

        }
        private bool Geen3TijdslotenEenzelfdeToestelNaElkaar(List<ReservatieDetail> alleDetails, ReservatieDetail nieuwDetail) {
            List<TimeSpan> einduren = alleDetails.Where(det => det.Datum == nieuwDetail.Datum && det.Toestel.Equals(nieuwDetail.Toestel)).Select(det => det.Tijdslot.Einduur).ToList();
            //.Contains(d.Tijdslot.Beginuur)).Select(y => y.Tijdslot.Einduur).OrderBy(t => t).ToList();
            bool rr;
            // Aangezien we maar 4 reservaties mogen doen, kunnen we in principe r[0] doen, want als er 2 elementen in de lijst zitten, dan
            // hebben we ofwel een foutieve registratie, ofwel al 4 registraties en dan houdt de andere methode het al tegen.

            // tijdslot in einduren is: 
            bool tweeTijdslotenVoorNieuweReservatieIsAanwezig = false;
            bool eenTijdslotVoorEenNieuwDetailIsAanwezig = false;
            bool eenTijdslotNaNieuweReservatieIsAanwezig = false;
            bool tweeTijdslotenNaNieuweReservatieIsAanwezig = false;
            foreach (TimeSpan ts in einduren) {
                //controlleren wanner het tijdslot valt.
                TimeSpan einduurVanReedsGereserveerd = new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds); // reftypes, dus dupliceren om er uren aan toe te voegen.
                if (einduurVanReedsGereserveerd == nieuwDetail.Tijdslot.Beginuur) {
                    eenTijdslotVoorEenNieuwDetailIsAanwezig = true;
                } else if (einduurVanReedsGereserveerd.Add(new TimeSpan(1, 0, 0)) == nieuwDetail.Tijdslot.Beginuur) {
                    tweeTijdslotenVoorNieuweReservatieIsAanwezig = true;
                } else if (einduurVanReedsGereserveerd.Add(new TimeSpan(-1, 0, 0)) == nieuwDetail.Tijdslot.Einduur) {
                    eenTijdslotNaNieuweReservatieIsAanwezig = true;
                } else if (einduurVanReedsGereserveerd == nieuwDetail.Tijdslot.Einduur) {
                    tweeTijdslotenNaNieuweReservatieIsAanwezig = true;
                }
            }
            // nu combinaties testen, drie mogelijkheden die niet kunnen | = reeds gereserveerd, 0 = nieuwReservatiedetail
            // ||0      0 ||      |0|    --> false
            if (tweeTijdslotenVoorNieuweReservatieIsAanwezig && eenTijdslotVoorEenNieuwDetailIsAanwezig) {
                rr = false;
            } else if (eenTijdslotNaNieuweReservatieIsAanwezig && eenTijdslotVoorEenNieuwDetailIsAanwezig) {
                rr = false;
            } else if (eenTijdslotNaNieuweReservatieIsAanwezig && tweeTijdslotenNaNieuweReservatieIsAanwezig) {
                rr = false;
            } else {
                rr = true;
            }

            if (!rr) {
                Console.WriteLine("stop");
            }
            return rr;
            
        }
        private bool MinderDan4ReservatiesOpDag(List<ReservatieDetail> alleDetails, ReservatieDetail detail) {
            bool r = 4 > alleDetails.Where(d => d.Datum == detail.Datum).Count();
            if (!r) {
                Console.WriteLine(""); //TODO
            }
            return r;
        }
        private bool IsVrijTijdslotVoorKlant(List<ReservatieDetail> alleDetails, ReservatieDetail detail) {
            bool r = !(alleDetails.Where(d => d.Tijdslot == detail.Tijdslot && d.Datum == detail.Datum).Count() > 0);
            if (!r) {
                Console.WriteLine(""); //TODO
            }
            return r;
        }
        public void SchrijfReservatieInDB() {
            Reservatie reservatie = new Reservatie(klant);
            foreach (ReservatieDetail detail in nieuweReservatiesKlant) {
                reservatie.VoegReservatieDetailToe(detail);
            }
            if (reservatie == null) { throw new ReservatieManagerException("SchrijfReservatieInDB - reservatie is null"); }
            //if (reservatie.Klant.ID <= 0) { throw new ReservatieManagerException("SchrijfReservatieInDB - klant ID is niet ingevuld"); }
            if (reservatie.ReservatieDetails.Count() == 0) { throw new ReservatieManagerException("SchrijfReservatieInDB - Geen reservatiedetails ingevuld"); }
            reservatieRepo.SchrijfReservatieInDB(reservatie);
            foreach (ReservatieDetail detail in nieuweReservatiesKlant) {
                detail.ZetIsNieuw(false);
            }
            toekomstigeReservatiesKlant = toekomstigeReservatiesKlant.Concat(nieuweReservatiesKlant).ToList();
            nieuweReservatiesKlant.Clear();
        }

        public void VoegToeAanNieuweReservatie(ReservatieDetail detail) {
            nieuweReservatiesKlant.Add(detail);
        }

        public List<ReservatieDetail> GeefReservatieDetailsVoorListBox() {
            return toekomstigeReservatiesKlant.Concat(nieuweReservatiesKlant).OrderBy(d => d.Datum).ThenBy(d => d.Tijdslot.Beginuur).ToList();
        }
    }
}

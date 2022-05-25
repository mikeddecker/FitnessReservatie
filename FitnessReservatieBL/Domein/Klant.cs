using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domein {
    public class Klant : Persoon {
        internal Klant(int iD, string voornaam, string achternaam, string email) : base(iD, voornaam, achternaam, email) {
            //ToekomstieReservaties = new List<Reservatie>();
        }
        //public List<Reservatie> ToekomstieReservaties { get; private set; }
        //public void VoegReservatieToe(Reservatie reservatie) {
        //    ToekomstieReservaties.Add(reservatie);
        //}

        //internal bool BevatReservatie(int reservatieID) {
        //    foreach (Reservatie r in ToekomstieReservaties) {
        //        if (r.ReservatieID == reservatieID) {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //internal Reservatie GeefReservatie(int reservatieID) {
        //    foreach (Reservatie r in ToekomstieReservaties) {
        //        if (r.ReservatieID == reservatieID) {
        //            return r;
        //        }
        //    }
        //    return null;
        //}
    }
}

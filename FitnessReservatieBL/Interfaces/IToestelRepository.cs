using FitnessReservatieBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces {
    public interface IToestelRepository {
        void VerwijderToestel(int toestelID);
        void UpdateToestelBeschikbaarheid(int toestelID, bool beschikbaar);
        bool HeeftToestelToekomstigeReservaties(int toestelID);
        int SchrijfToestelInDB(Toestel nieuwToestel);
    }
}

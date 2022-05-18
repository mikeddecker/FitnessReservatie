using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieBL.Managers;
using FitnessReservatieDL;
using System;
using System.Collections.Generic;

namespace ReservatieSchrijvenInDBTest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            string connectionString = @"Data Source=LAPTOP-BFPIKR71\SQLEXPRESS;Initial Catalog=FitnessReservatie;Integrated Security=True";
            IFitnessRepository fitnessRepo = new FitnessRepoADO(connectionString);
            IPersoonRepository persoonRepo = new PersoonRepoADO(connectionString);
            IReservatieRepository reservatieRepo = new ReservatieRepoADO(connectionString);
            FitnessManager fitnessManager = new FitnessManager(fitnessRepo);
            PersoonManager persoonManager = new PersoonManager(persoonRepo);
            ReservatieManager reservatieManager = new ReservatieManager(reservatieRepo);
            IReadOnlyList<Toestel> toestellen = fitnessManager.GeefToestellen();
            IReadOnlyList<Tijdslot> tijdsloten = fitnessManager.GeefTijdsloten();
            Toestel t3 = toestellen[2];
            Toestel t4 = toestellen[3];
            Tijdslot ts10 = tijdsloten[9];
            Tijdslot ts11 = tijdsloten[10];
            ReservatieDetail detail1 = new ReservatieDetail(new DateTime(2022, 05, 18), ts10, t3);
            ReservatieDetail detail2 = new ReservatieDetail(new DateTime(2022, 05, 18), ts11, t4);
            Klant k = (Klant)persoonManager.LogPersoonIn("mike.dedecker@student.hogent.be");
            Reservatie reservatie = new Reservatie(k);
            reservatie.VoegReservatieDetailToe(detail1);
            //reservatie.VoegReservatieDetailToe(detail2);
            reservatieManager.SchrijfReservatieInDB(reservatie);
        }
    }
}

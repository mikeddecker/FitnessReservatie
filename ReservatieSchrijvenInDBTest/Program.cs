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
            
            IPersoonRepository persoonRepo = new PersoonRepoADO(connectionString);
            IReservatieRepository reservatieRepo = new ReservatieRepoADO(connectionString);
            
            PersoonManager persoonManager = new PersoonManager(persoonRepo);
            ReservatieManager reservatieManager = new ReservatieManager(reservatieRepo);

            IReadOnlyList<Tijdslot> tijdsloten = reservatieManager.GeefTijdsloten();
            Toestel t5 = new Toestel("toestel 5", true);
            t5.ZetId(5);
            Toestel t4 = new Toestel("toestel 4", true);
            t4.ZetId(4);
            Tijdslot ts10 = tijdsloten[9];
            Tijdslot ts11 = tijdsloten[10];
            ReservatieDetail detail1 = new ReservatieDetail(new DateTime(2022, 05, 25), ts10, t5);
            ReservatieDetail detail2 = new ReservatieDetail(new DateTime(2022, 05, 25), ts11, t4);
            Klant k = (Klant)persoonManager.LogPersoonIn("mike.dedecker@student.hogent.be");
            Reservatie reservatie = new Reservatie(k);
            reservatie.VoegReservatieDetailToe(detail1);
            reservatie.VoegReservatieDetailToe(detail2);
            reservatieManager.SchrijfReservatieInDB(reservatie);
        }
    }
}

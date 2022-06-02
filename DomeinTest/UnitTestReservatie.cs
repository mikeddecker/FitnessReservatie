using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomeinTest {
    public class UnitTestReservatie {
        [Fact]
        public void ZetReservatieID_valid() {
            Klant k = new Klant(3, "Mike", "De Decker", "mike.dedecker@student.hogent.be");
            Reservatie r = new Reservatie(k);
            r.ZetReservatieID(6);
            Assert.Equal(6, r.ReservatieID);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ZetReservatieID_invalid(int id) {
            Klant k = new Klant(3, "Mike", "De Decker", "mike.dedecker@student.hogent.be");
            Reservatie r = new Reservatie(k);
            Assert.Throws<ReservatieException>(() => r.ZetReservatieID(id));
        }

        [Fact]
        public void ZetKlant_valid() {
            Klant k = new Klant(3, "Mike", "De Decker", "mike.dedecker@student.hogent.be");
            Reservatie r = new Reservatie(k);
            Assert.Equal(k, r.Klant);
            Klant kkkk = new Klant(4, "Jan", "De Decker", "jan.dedecker@familie.be");
            r.ZetKlant(kkkk);
            Assert.Equal(kkkk, r.Klant);

        }
        [Fact]
        public void ZetKlant_invalid() {
            Klant k = new Klant(3, "Mike", "De Decker", "mike.dedecker@student.hogent.be");
            Reservatie r = new Reservatie(k);
            Assert.Equal(k, r.Klant);
            Assert.Throws<ReservatieException>(() => r.ZetKlant(null));
        }

        [Fact]
        public void VoegReservatieDetailToe_valid() {
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            ReservatieDetail detail2 = new ReservatieDetail(datum, ts, t);
            Klant k = new Klant(3, "Mike", "De Decker", "mike.dedecker@student.hogent.be");
            Reservatie r = new Reservatie(k);
            r.VoegReservatieDetailToe(detail);
            Assert.Contains(detail2, r.ReservatieDetails);
        }

        [Fact]
        public void VoegReservatieDetailToe_invalid() {
            // prepareren
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            ReservatieDetail detail2 = new ReservatieDetail(datum, ts, t);
            Klant k = new Klant(3, "Mike", "De Decker", "mike.dedecker@student.hogent.be");
            Reservatie r = new Reservatie(k);
            r.VoegReservatieDetailToe(detail);

            Assert.Throws<ReservatieException>(() => r.VoegReservatieDetailToe(null));
            Assert.Throws<ReservatieException>(() => r.VoegReservatieDetailToe(detail2));
        }

        //[Theory]
        //Toch maar 1 lijntje, maar te lui om weg te doen grtjs...
        //[InlineData(3,"Mike", "De Decker", "mike.dedecker@student.hogent.be", 3, "Mike", "De Decker", "mike.dedecker@student.hogent.be")]
        [Fact]
        public void Ctor_valid() {
            Reservatie r = new Reservatie(new Klant(3, "Mike", "De Decker", "mike.DEDECKER@student.hogent.be"));
            Assert.Equal(3, r.Klant.ID);
            Assert.Equal("Mike", r.Klant.Voornaam);
            Assert.Equal("De Decker", r.Klant.Achternaam);
            Assert.Equal("mike.dedecker@student.hogent.be", r.Klant.Email);
            Assert.NotNull(r.ReservatieDetails);
            Assert.Empty(r.ReservatieDetails);
        }

        [Fact]
        public void Ctor_invalid() {
            Assert.Throws<ReservatieException>(() => new Reservatie(null));
        }
    }
}

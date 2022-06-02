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

        //TODO [Fact] public void VoegReservatieDetailToe

    }
}

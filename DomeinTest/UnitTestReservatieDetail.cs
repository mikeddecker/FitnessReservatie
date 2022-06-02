using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomeinTest {
    public class UnitTestReservatieDetail {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(7)]
        public void ZetDatum_valid(int dagenInDeToekomst) {
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            Assert.Equal(datum, detail.Datum);
            detail.ZetDatum(DateTime.Today.AddDays(dagenInDeToekomst));
            Assert.Equal(DateTime.Today.AddDays(dagenInDeToekomst), detail.Datum);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(8)]
        public void ZetDatum_invalid(int dagenInDeToekomst) {
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            Assert.Equal(datum, detail.Datum);
            Assert.Throws<ReservatieDetailException>(() => detail.ZetDatum(DateTime.Today.AddDays(dagenInDeToekomst)));
        }
        [Fact]
        public void ZetToestel_valid() {
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            Assert.Equal(t, detail.Toestel);
            Toestel toestel = new Toestel("fiets", true);
            detail.ZetToestel(toestel);
            Assert.Equal(toestel, detail.Toestel);
        }
        [Fact]
        public void ZetToestel_invalid() {
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            Assert.Equal(t, detail.Toestel);
            Assert.Throws<ReservatieDetailException>(() => detail.ZetToestel(null));

        }
        [Fact]
        public void ZetTijdslot_valid() {
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            Assert.Equal(ts, detail.Tijdslot);
            Tijdslot tijdslot = new Tijdslot(3, new TimeSpan(8,0,0), new TimeSpan(9, 0, 0));
            detail.ZetTijdslot(tijdslot);
            Assert.Equal(tijdslot, detail.Tijdslot);
        }
        [Fact]
        public void ZetTijdslot_invalid() {
            Toestel t = new Toestel("loopband", true);
            Tijdslot ts = new Tijdslot(14, new TimeSpan(21, 0, 0), new TimeSpan(22, 0, 0));
            DateTime datum = DateTime.Today.AddDays(3);
            ReservatieDetail detail = new ReservatieDetail(datum, ts, t);
            Assert.Equal(ts, detail.Tijdslot);
            Assert.Throws<ReservatieDetailException>(() => detail.ZetTijdslot(null));            
        }

    }
}

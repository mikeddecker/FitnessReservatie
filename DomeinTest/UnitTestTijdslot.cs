using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomeinTest {
    public class UnitTestTijdslot {
        [Fact]
        public void ZetID_valid() {
            Tijdslot t = new Tijdslot(6, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0));
            Assert.Equal(6, t.TijdslotID);
            t.ZetID(1);
            Assert.Equal(1, t.TijdslotID);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ZetID_invalid(int id) {
            Tijdslot t = new Tijdslot(6, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0));
            Assert.Equal(6, t.TijdslotID);
            Assert.Throws<TijdslotException>(() => t.ZetID(id));
        }

        [Fact]
        public void ZetBeginuur_valid() {
            Tijdslot t = new Tijdslot(6, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0));
            Assert.Equal(new TimeSpan(8, 0, 0), t.Beginuur);
            TimeSpan ts = new TimeSpan(7, 0, 0);
            t.ZetBeginuur(ts);
            Assert.Equal(ts, t.Beginuur);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(25)]
        public void ZetBeginuur_invalid(int beginuur) {
            Tijdslot t = new Tijdslot(6, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0));
            Assert.Throws<TijdslotException>(() => t.ZetBeginuur(new TimeSpan(beginuur, 0, 0)));
        }

        [Fact]
        public void ZetEinduur_valid() {
            Tijdslot t = new Tijdslot(6, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0));
            Assert.Equal(new TimeSpan(9, 0, 0), t.Einduur);
            TimeSpan ts = new TimeSpan(10, 0, 0);
            t.ZetEinduur(ts);
            Assert.Equal(ts, t.Einduur);
        }
        [Theory]
        [InlineData(7)]
        [InlineData(25)]
        public void ZetEinduur_invalid(int einduur) {
            Tijdslot t = new Tijdslot(6, new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0));
            Assert.Equal(new TimeSpan(9, 0, 0), t.Einduur);
            Assert.Throws<TijdslotException>(() => t.ZetEinduur(new TimeSpan(einduur, 0, 0)));
        }

        [Fact]
        public void Ctor_valid() {
            TimeSpan beginuur = new TimeSpan(2, 2, 2);
            TimeSpan einduur = new TimeSpan(3, 3, 3);
            Tijdslot t = new Tijdslot(1, beginuur, einduur);
            Assert.Equal(1, t.TijdslotID);
            Assert.Equal(beginuur, t.Beginuur);
            Assert.Equal(einduur, t.Einduur);
        }

        [Theory]
        [InlineData(-1, 8, 9)]
        [InlineData(0, 8, 9)]
        [InlineData(1, 10, 9)]
        [InlineData(1, 25, 9)]
        [InlineData(1, 8, 7)]
        [InlineData(1, 8, 25)]
        public void Ctor_invalid(int id, int beginuur, int einduur) {
            Assert.Throws<TijdslotException>(() => new Tijdslot(id, new TimeSpan(beginuur,0,0), new TimeSpan(einduur,0,0)));
        }
    }
}

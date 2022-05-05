using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomeinTest {
    public class UnitTestToestel {
        [Theory]
        [InlineData("Loopband", "Loopband")]
        [InlineData("Loopband     ", "Loopband")]
        [InlineData("Loopband ", "Loopband")]
        [InlineData("     Loopband", "Loopband")]
        public void ZetToestelnaam_valid(string naamIn, string naamUit) {
            Toestel k = new Toestel("Fiets", "images/url1");
            Assert.Equal("Fiets", k.Toestelnaam);
            k.ZetToestelnaam(naamIn);
            Assert.Equal(naamUit, k.Toestelnaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetToestelnaam_invalid(string toestelnaamnaam) {
            Toestel toestel = new Toestel("fiets", "urls/fiets");
            Assert.Throws<ToestelException>(() => toestel.ZetToestelnaam(toestelnaamnaam));
        }
    }
}

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
        [Fact]
        public void ZetId_valid() {
            Toestel t = new Toestel("fiets", true);
            t.ZetId(3);
            Assert.Equal(3, t.ToestelID);
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(0)]
        public void ZetId_invalid(int id) {
            Toestel t  = new Toestel("fiets", true);
            Assert.Throws<ToestelException>(() => t.ZetId(id));
        }
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void ZetBeschikbaarheid_valid(bool isBeschikbaarIn, bool isBeschikbaarUit) {
            Toestel k = new Toestel("fiets", !isBeschikbaarIn);
            k.ZetBeschikbaarheid(isBeschikbaarIn);
            Assert.True(k.Beschikbaar == isBeschikbaarUit);
        }
        [Theory]
        [InlineData("loopband", "loopband")]
        [InlineData("Loopband", "loopband")]
        [InlineData("loopband     ", "loopband")]
        [InlineData("loopband ", "loopband")]
        [InlineData("     Loopband", "loopband")]
        public void ZetToestelnaam_valid(string naamIn, string naamUit) {
            Toestel k = new Toestel("fiets", true);
            Assert.Equal("fiets", k.Type);
            k.ZetType(naamIn);
            Assert.Equal(naamUit, k.Type);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetToestelnaam_invalid(string toestelnaamnaam) {
            Toestel toestel = new Toestel("fiets", true);
            Assert.Throws<ToestelException>(() => toestel.ZetType(toestelnaamnaam));
        }

        //[Theory]
        //[InlineData("afbeeldingUrl", "afbeeldingUrl")]
        //[InlineData("afbeeldingUrl     ", "afbeeldingUrl")]
        //[InlineData("afbeeldingUrl ", "afbeeldingUrl")]
        //[InlineData("     afbeeldingUrl", "afbeeldingUrl")]
        //public void ZetAfbeeldingUrl_valid(string urlIn, string urlUit) {
        //    // in feite: Path.Exists
        //    //Assert.True(Path.Exists(urlIn));
        //    // TODO eventueel afbeeldingen toevoegen
        //    Toestel toestel = new Toestel("Fiets", "images/url1");
        //    Assert.Equal("images/url1", toestel.AfbeeldingUrl);
        //    toestel.ZetAfbeeldingUrl(urlIn);
        //    Assert.Equal(urlUit, toestel.AfbeeldingUrl);
        //}

        //[Theory]
        //[InlineData("")]
        //[InlineData(" ")]
        //[InlineData("\n")]
        //[InlineData("   \r   ")]
        //[InlineData(null)]
        //public void ZetAfbeeldingUrl_invalid(string url) {
        //    Toestel t = new Toestel("fiets", "url");
        //    Assert.Equal("url", t.AfbeeldingUrl);
        //    Assert.Throws<ToestelException>(() => t.ZetAfbeeldingUrl(url));
        //}

        [Fact]
        public void Ctor_valid() {
            Toestel t = new Toestel("loopband", true);
            Assert.Equal("loopband", t.Type);
            Assert.True(t.Beschikbaar);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void Ctor_invalid(string toestelnaam) { 
            Assert.Throws<ToestelException>(() => new Toestel(toestelnaam, false));
        }
    }
}

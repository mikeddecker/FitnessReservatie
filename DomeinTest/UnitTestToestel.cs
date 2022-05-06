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

        [Theory]
        [InlineData("afbeeldingUrl", "afbeeldingUrl")]
        [InlineData("afbeeldingUrl     ", "afbeeldingUrl")]
        [InlineData("afbeeldingUrl ", "afbeeldingUrl")]
        [InlineData("     afbeeldingUrl", "afbeeldingUrl")]
        public void ZetAfbeeldingUrl_valid(string urlIn, string urlUit) {
            // in feite: Path.Exists
            //Assert.True(Path.Exists(urlIn));
            // TODO
            Toestel toestel = new Toestel("Fiets", "images/url1");
            Assert.Equal("images/url1", toestel.AfbeeldingUrl);
            toestel.ZetAfbeeldingUrl(urlIn);
            Assert.Equal(urlUit, toestel.AfbeeldingUrl);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetAfbeeldingUrl_invalid(string url) {
            Toestel t = new Toestel("fiets", "url");
            Assert.Equal("url", t.AfbeeldingUrl);
            Assert.Throws<ToestelException>(() => t.ZetAfbeeldingUrl(url));
        }

        [Fact]
        public void Ctor_valid() {
            Toestel t = new Toestel("loopband", "loopband url");
            Assert.Equal("loopband", t.Toestelnaam);
            Assert.Equal("loopband url", t.AfbeeldingUrl);
        }

        [Theory]
        [InlineData("", "goede url")]
        [InlineData(" ", "goede url")]
        [InlineData("\n", "goede url")]
        [InlineData("   \r   ", "goede url")]
        [InlineData(null, "goede url")]
        [InlineData("loopband", "")]
        [InlineData("loopband", " ")]
        [InlineData("loopband", "\n")]
        [InlineData("loopband", "   \r   ")]
        [InlineData("loopband", null)]
        public void Ctor_invalid(string toestelnaam, string url) {
            Assert.Throws<ToestelException>(() => new Toestel(toestelnaam, url));
        }
    }
}

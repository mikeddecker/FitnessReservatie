using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomeinTest {
    public class UnitTestKlant {
        [Fact]
        public void ZetId_valid() {
            Klant k = new Klant(1, "mike", "de decker", "mikeddecker@hotmail.com");
            Assert.Equal(1, k.ID);
            k.ZetID(3);
            Assert.Equal(3, k.ID);
        }

        [Fact]
        public void ZetId_invalid() {
            Klant k = new Klant(6, "jan", "jansens", "jan.janssens@email.com");
            Assert.Equal(6, k.ID);
            Assert.Throws<KlantException>(() => k.ZetID(-3));
        }

        [Theory]
        [InlineData("Jan", "Jan")]
        [InlineData("Janneke     ", "Janneke")]
        [InlineData("     Janneke", "Janneke")]
        public void ZetVoornaam_valid(string naamIn, string naamUit) {
            Klant k = new Klant(96, "Jos", "Joskens", "eenmaail@mail.me");
            Assert.Equal("Jos", k.Voornaam);
            k.ZetVoornaam(naamIn);
            Assert.Equal(naamUit, k.Voornaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetVoornaam_invalid(string naam) {
            Klant k = new Klant(10, "Jos", "Joskens", "mail@mail.mail");
            Assert.Throws<KlantException>(() => k.ZetVoornaam(naam));
        }

        [Theory]
        [InlineData("Jansen", "Jansen")]
        [InlineData("Jannekes     ", "Jannekes")]
        [InlineData("De Decker ", "De Decker")]
        [InlineData("     Jannekes", "Jannekes")]
        public void ZetAchternaam_valid(string naamIn, string naamUit) {
            Klant k = new Klant(96, "Jos", "Joskens", "eenmaail@mail.me");
            Assert.Equal("Joskens", k.Achternaam);
            k.ZetAchternaam(naamIn);
            Assert.Equal(naamUit, k.Achternaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetAchternaam_invalid(string naam) {
            Klant k = new Klant(10, "Jos", "Joskens", "mail@mail.mail");
            Assert.Throws<KlantException>(() => k.ZetAchternaam(naam));
        }

        [Theory]
        [InlineData("mikeddecker@hotmail.com", "mikeddecker@hotmail.com")]
        [InlineData("   mikeddecker@hotmail.com", "mikeddecker@hotmail.com")]
        [InlineData("mikeddecker@hotmail.com   ", "mikeddecker@hotmail.com")]
        [InlineData("mike@gmail.student.com", "mike@gmail.student.com")]
        [InlineData("mike.dedecker@student.hogent.be", "mike.dedecker@student.hogent.be")]
        public void ZetEmail_valid(string emailIn, string emailUit) {
            Klant k = new Klant(3, "mike", "de decker", "ik@gmail.com");
            Assert.Equal("ik@gmail.com", k.Email);
            k.ZetEmail(emailIn);
            Assert.Equal(emailUit, k.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        [InlineData("mike")]
        [InlineData("mike@im")]
        [InlineData("@im")]
        [InlineData("mike@")]
        [InlineData("mike@.be")]
        public void ZetEmail_invalid(string email) {
            Klant k = new Klant(3, "mike", "de decker", "ik@gmail.com");
            Assert.Throws<KlantException>(() => k.ZetEmail(email));
        }
    }
}

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

        [Theory]
        [InlineData(-3)]
        [InlineData(0)]
        public void ZetId_invalid(int id) {
            Klant k = new Klant(6, "jan", "jansens", "jan.janssens@email.com");
            Assert.Equal(6, k.ID);
            Assert.Throws<KlantException>(() => k.ZetID(id));
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

        [Fact]
        public void Ctor_valid() {
            Klant k = new Klant(2, "Mike", "De Decker", "mike@hotmail.com");
            Assert.Equal(2, k.ID);
            Assert.Equal("Mike", k.Voornaam);
            Assert.Equal("De Decker", k.Achternaam);
            Assert.Equal("mike@hotmail.com", k.Email);
        }

        [Theory]
        [InlineData(-1, "Jan", "Janssens", "jan.janssens@gmail.com")]
        [InlineData(0, "Jan", "Janssens", "jan.janssens@gmail.com")]
        [InlineData(1, "", "Janssens", "jan.janssens@gmail.com")]
        [InlineData(1, "   ", "Janssens", "jan.janssens@gmail.com")]
        [InlineData(1, "\n", "Janssens", "jan.janssens@gmail.com")]
        [InlineData(1, "     \r        ", "Janssens", "jan.janssens@gmail.com")]
        [InlineData(1, null, "Janssens", "jan.janssens@gmail.com")]
        [InlineData(1, "Jan", "", "jan.janssens@gmail.com")]
        [InlineData(1, "Jan", "   ", "jan.janssens@gmail.com")]
        [InlineData(1, "Jan", "\n", "jan.janssens@gmail.com")]
        [InlineData(1, "Jan", "   \r ", "jan.janssens@gmail.com")]
        [InlineData(1, "Jan", null, "jan.janssens@gmail.com")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmailcom")]
        [InlineData(1, "Jan", "Janssens", "")]
        [InlineData(1, "Jan", "Janssens", " ")]
        [InlineData(1, "Jan", "Janssens", "\n")]
        [InlineData(1, "Jan", "Janssens", "  \r   ")]
        [InlineData(1, "Jan", "Janssens", null)]
        [InlineData(1, "Jan", "Janssens", "jan@")]
        [InlineData(1, "Jan", "Janssens", "@gmailcom")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@.gmailcom")]
        [InlineData(1, "Jan", "Janssens", "jan@gmailcom")]
        [InlineData(1, "Jan", "Janssens", "jangmailcom")]
        public void Ctor_invalid(int id, string voornaam, string achternaam, string email) {
            Assert.Throws<KlantException>(() => new Klant(id, voornaam, achternaam, email));
        }
    }
}

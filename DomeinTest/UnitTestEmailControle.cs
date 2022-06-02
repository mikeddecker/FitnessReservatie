using FitnessReservatieBL.Domein;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomeinTest {
    public class UnitTestEmailControle {
        [Theory]
        [InlineData("mikeddecker@hotmail.com")]
        [InlineData("   mikeddecker@hotmail.com")]
        [InlineData("mikeddecker@hotmail.com   ")]
        [InlineData("mike@gmail.student.com")]
        [InlineData("mike.dedecker@student.hogent.be")]
        [InlineData("MIKE.dedecker@student.hogent.be")]
        public void ControleerEmail_valid(string email) {
            Assert.True(EmailControle.ControleerEmail(email));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData("mike")]
        [InlineData("mike@im")]
        [InlineData("@im")]
        [InlineData("mike@")]
        [InlineData("mike@.be")]
        public void ControleerEmail_invalid(string email) {
            Assert.Throws<EmailControleException>(() => EmailControle.ControleerEmail(email));
        }
    }
}

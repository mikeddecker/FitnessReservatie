using System;
using System.Net;
using System.Net.Mail;

namespace MailAdresTest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            Console.WriteLine(IsValid("server1"));
            Console.WriteLine(IsValid("server1 @"));
            Console.WriteLine(IsValid("@student.be"));
            Console.WriteLine(IsValid("server1@mail"));
        }
        public static bool IsValid(string emailaddress) {
            try {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            } catch (FormatException) {
                return false;
            }
        }
    }
}

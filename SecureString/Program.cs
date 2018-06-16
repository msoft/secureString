using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStringExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //var passwordChecker = new WithSimpleString.PasswordChecker(
            //    WithSimpleString.PasswordFinderFactory.GetFactory());
            var passwordChecker = new WithSecureString.PasswordChecker(
                WithSecureString.PasswordFinderFactory.GetFactory());

            bool passwordIsOk = false;
            int tryCount = 0;

            while (!passwordIsOk && tryCount < 3)
            {
                Console.WriteLine($"Essai {tryCount + 1}");
                Console.WriteLine("Quel est le login ?");
                string login = Console.ReadLine();

                Console.WriteLine("Quel est le mot de passe ?");
                string password = Console.ReadLine();
                passwordIsOk = passwordChecker.CheckPassword(login, password);
                if (passwordIsOk)
                {
                    Console.WriteLine("OK");
                    break;
                }
                else
                {
                    Console.WriteLine("KO");
                    tryCount++;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStringExample.WithSimpleString
{
    public class PasswordChecker
    {
        private IPasswordFinder passwordFinder;
        
        public PasswordChecker(PasswordFinderFactory passwordFinderFactory)
        {
            this.passwordFinder = passwordFinderFactory.GetNewPasswordFinder();
        }

        public bool CheckPassword(string login, string password)
        {
            var foundPassword = this.passwordFinder.GetPassword(login);
            return string.IsNullOrEmpty(foundPassword) ? false : foundPassword.Equals(password);
        }
    }
}

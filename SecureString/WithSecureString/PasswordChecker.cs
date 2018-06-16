using SecureStringExample.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecureStringExample.WithSecureString
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
            if (foundPassword == null)
                return false;
            else
            {
                //return SecureStringHelper.UseSecureStringContent<bool>(
                //    foundPassword,
                //    p => password.ToArray().SequenceEqual(p));
                return SecureStringHelper.UseSecureString(
                    foundPassword,
                    p => password == p);
            }
                
        }
    }
}

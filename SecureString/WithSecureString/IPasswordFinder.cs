using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SecureStringExample.WithSecureString
{
    public interface IPasswordFinder
    {
        SecureString GetPassword(string login);
    }
}

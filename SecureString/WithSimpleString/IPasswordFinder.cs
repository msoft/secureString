using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStringExample.WithSimpleString
{
    public interface IPasswordFinder
    {
        string GetPassword(string login);
    }
}

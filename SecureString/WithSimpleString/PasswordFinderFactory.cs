using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStringExample.WithSimpleString
{
    public class PasswordFinderFactory
    {
        private readonly Dictionary<string, string> passwordPerUser = new Dictionary<string, string>();

        public void SetPassword(string login, string password)
        {
            this.passwordPerUser[login] = password;
        }

        public IPasswordFinder GetNewPasswordFinder()
        {
            var passwordFinderMock = new Mock<IPasswordFinder>();
            foreach (var user in this.passwordPerUser)
                passwordFinderMock.Setup(f => f.GetPassword(user.Key)).Returns(user.Value);

            return passwordFinderMock.Object;
        }

        public static PasswordFinderFactory GetFactory()
        {
            var factory = new PasswordFinderFactory();


            factory.SetPassword("User1", $"{(char)109}{(char)121}{(char)115}{(char)101}{(char)99}{(char)114}{(char)101}{(char)116}{(char)112}{(char)97}{(char)115}{(char)115}{(char)119}{(char)111}{(char)114}{(char)100}");
            factory.SetPassword("User2", $"{(char)111}{(char)116}{(char)104}{(char)101}{(char)114}{(char)112}{(char)97}{(char)115}{(char)115}{(char)119}{(char)111}{(char)114}{(char)100}");

            return factory;
        }
    }
}

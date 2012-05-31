using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlSystem;
using NUnit.Framework;
using Rhino.Mocks;

namespace SIR.Tests
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void Admin_Constructor_NameTest()
        {
            IUser _user = new Admin();
            string _str = "Admin";
            _user.userName = _str;

            Assert.AreEqual(_str, _user.userName);
        }

        [Test]
        public void User_Constructor_NameTest()
        {
            IUser _user = new User();
            string _str = "User";
            _user.userName = _str;

            Assert.AreEqual(_str, _user.userName);
        }
    }
}

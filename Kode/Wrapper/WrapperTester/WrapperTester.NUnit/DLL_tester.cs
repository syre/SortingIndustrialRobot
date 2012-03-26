using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WrapperTester.NUnit
{
    [TestFixture]
    public class DLL_tester
    {
        private DLL _dll;

        [Setup]
        public void SetupTest()
        {
            _dll = new DLL();
            
        }

        [TearDown]
        public void ExitTest()
        {
            //_dll = null;
        }

        [Test]
        public void Initailization()
        {
            Assert.AreNotEqual();
        }

        [Test]
        public void Control()
        {
            
        }

        [Test]
        public void Home()
        {

        }

        [Test]
        public void OpenGripper()
        {

        }

        [Test]
        public void CloseGripper()
        {

        }

        [Test]
        public void GetJaw()
        {

        }

        [Test]
        public void EnterManual()
        {

        }

        [Test]
        public void CloseManual()
        {

        }

        [Test]
        public void MoveManual()
        {

        }

        [Test]
        public void WatchMotion()
        {

        }

        [Test]
        public void WatchDigitalInput()
        {

        }

        [Test]
        public void CLoseWatchDigitalInput()
        {

        }

        [Test]
        public void IsOnLineOK()
        {

        }

        [Test]
        public void MoveLinear()
        {

        }

        [Test]
        public void DefineVector()
        {

        }

        [Test]
        public void Teach()
        {

        }

        [Test]
        public void GetCurrentPosition()
        {

        }

    }
}

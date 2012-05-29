using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Command;
using GalaSoft.MvvmLight;



namespace SIR.Tests
{   
    [TestFixture]
    public class InOutService_Test
    {
        private InOutService _IOS;
        [SetUp]
        public void SetUp()
        {

            //_IOS = new InOutService();
           
            // _IOS = MockRepository.GenerateMock<InOutService>();
        }

        [TearDown]
        public void TearDown()
        {
            _IOS = null;
        }

        [Test]
        public void InOutService_SaveFile_CreatedFile()
        {
            
        }

    }
}

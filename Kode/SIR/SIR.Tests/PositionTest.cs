using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSL;
using NUnit.Framework;
using Rhino.Mocks;

namespace SIR.Tests
{
    [TestFixture]
    public class PositionTest
    {
        private PositionModel _positionModel;
        private PositionViewModel _positionViewModel;

        [SetUp]
        public void SetUp()
        {
            _positionModel = MockRepository.GenerateStub<PositionModel>();
            _positionViewModel = new PositionViewModel(_positionModel);
        }


        [TearDown]
        public void TearDown()
        {
            _positionModel = null;
            _positionViewModel = null;
        }

        [Test]
        public void PositionModel_StubTest()
        {
            int iX = 10;
            int iY = 20;
            int iZ = 30;
            int iPitch = 40;
            int iRoll = 50;
            VecPoint _vec = MockRepository.GenerateStub<VecPoint>(iX, iY, iZ, iPitch, iRoll);
            _positionViewModel.update(_vec);

            Assert.AreSame(_vec, _positionModel.PositionVec);
        }

        [Test]
        public void PositionViewModel_XYZ()
        {
            int iX = 10;
            int iY = 20;
            int iZ = 30;
            int iPitch = 40;
            int iRoll = 50;
            VecPoint _vec = MockRepository.GenerateStub<VecPoint>(iX, iY, iZ, iPitch, iRoll);
            _positionViewModel.update(_vec);
            string str = iX.ToString() + ";" + iY.ToString() + ";" + iZ.ToString();

            Assert.AreEqual(str, _positionViewModel.XYZ);
        }

        [Test]
        public void PositionViewModel_XYZPR()
        {
            int iX = 10;
            int iY = 20;
            int iZ = 30;
            int iPitch = 40;
            int iRoll = 50;
            VecPoint _vec = MockRepository.GenerateStub<VecPoint>(iX, iY, iZ, iPitch, iRoll);
            _positionViewModel.update(_vec);
            string str = iX.ToString() + ";" + iY.ToString() + ";" + iZ.ToString() + ";" + iPitch.ToString() + ";" +
                         iRoll.ToString();

            Assert.AreEqual(str, _positionViewModel.XYZPR);
        }

    }

}

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
    public class wrapperVectorTest
    {

        //Test For RelCoordSirVector
        [Test]
        public void RelCoordSirVector_Constructor_GetNameTest()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            RelCoordSirVector _relCoordSirVector = new RelCoordSirVector("TestOne");

            _relCoordSirVector.addPoint(_vecPoints);

            Assert.AreEqual("TestOne", _relCoordSirVector.Name);
        }

        [Test]
        public void RelCoordSirVector_Constructor_GetiType()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            RelCoordSirVector _relCoordSirVector = new RelCoordSirVector("TestOne");

            Assert.AreEqual(-32767, _relCoordSirVector.Type);
        }

        [Test]
        public void RelCoordSirVector_getSize_AddOneToList_GetOneFromCount()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10,10,10,10,10);
            RelCoordSirVector _relCoordSirVector = new RelCoordSirVector("TestOne");

            _relCoordSirVector.addPoint(_vecPoints);

            Assert.AreEqual(1, _relCoordSirVector.getSize());
        }

        [Test]
        public void RelCoordSirVector_getPoint_AddPoint_TestIfSame()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            RelCoordSirVector _relCoordSirVector = new RelCoordSirVector("TestOne");

            _relCoordSirVector.addPoint(_vecPoints);

            Assert.AreEqual(_vecPoints, _relCoordSirVector.getPoint(0));
        }

        //Test For AbsCoordSirVector
        [Test]
        public void AbsCoordSirVector_Constructor_GetNameTest()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            AbsCoordSirVector _absCoordSirVector = new AbsCoordSirVector("TestOne");

            _absCoordSirVector.addPoint(_vecPoints);

            Assert.AreEqual("TestOne", _absCoordSirVector.Name);
        }

        [Test]
        public void AbsCoordSirVector_Constructor_GetiType()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            AbsCoordSirVector _absCoordSirVector = new AbsCoordSirVector("TestOne");

            Assert.AreEqual(-32766, _absCoordSirVector.Type);
        }

        [Test]
        public void AbsCoordSirVector_getSize_AddOneToList_GetOneFromCount()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            AbsCoordSirVector _absCoordSirVector = new AbsCoordSirVector("TestOne");

            _absCoordSirVector.addPoint(_vecPoints);

            Assert.AreEqual(1, _absCoordSirVector.getSize());
        }

        [Test]
        public void AbsCoordSirVector_getPoint_AddPoint_TestIfSame()
        {
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            AbsCoordSirVector _absCoordSirVector = new AbsCoordSirVector("TestOne");

            _absCoordSirVector.addPoint(_vecPoints);

            Assert.AreEqual(_vecPoints, _absCoordSirVector.getPoint(0));
        }

    }
}

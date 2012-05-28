using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlSystem;
using NUnit.Framework;
using Rhino.Mocks;
using System.IO.Ports;

namespace SIR.Tests
{   
    [TestFixture]
    public class serialSTKTest
    {
        [Test]
        public void SerielSTK_Constructor()
        {
            SerialPort sp = MockRepository.GenerateStub<SerialPort>("COM1", 9600, Parity.None, 8, StopBits.One);
            SerialSTK serialStk = new SerialSTK();
            serialStk.serialPort = sp;

            Assert.AreEqual("COM1", serialStk.serialPort.PortName);
            Assert.AreEqual(9600, serialStk.serialPort.BaudRate);
        }

        [Test]
        public void SerielSTK_Open()
        {
            SerialPort sp = MockRepository.GenerateStub<SerialPort>("COM1", 9600, Parity.None, 8, StopBits.One);
            SerialSTK serialStk = new SerialSTK();
            sp.Stub(t => t.Open()).Return(true);
            serialStk.serialPort = sp;
            

            Assert.IsTrue(serialStk.Open());
        }

    }
}

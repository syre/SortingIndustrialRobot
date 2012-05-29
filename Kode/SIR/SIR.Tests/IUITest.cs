using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlSystem;
using NUnit.Framework;
using Rhino.Mocks;
using System.ComponentModel;

namespace SIR.Tests
{   
    [TestFixture]
    public class IUITest
    {
        [Test]
        public void StringUI_Constructor_Test()
        {
            StringUI _stringUI = new StringUI();
            Assert.IsEmpty(_stringUI.Buffer);
        }

        [Test]
        public void StringUI_WriteLine_BufferAreSame()
        {
            StringUI _stringUI = new StringUI();
            object[] Array = new[] { "1", "2" };

            _stringUI.writeLine("Hello {0}", Array);
            _stringUI.writeLine("Hello {1}", Array);

            Assert.AreEqual("* Hello 1\r\n* Hello 2\r\n", _stringUI.Buffer);
        }

        [Test]
        public void StringUI_WriteLine_ThenClear_BufferIsEmpty()
        {
            StringUI _stringUI = new StringUI();

            object[] Array = new[] { "1", "2" };

            _stringUI.writeLine("Hello {0}", Array);
            _stringUI.writeLine("Hello {1}", Array);

            _stringUI.clearString();

            Assert.IsEmpty(_stringUI.Buffer);
        }

        [Test]
        public void StringUI_ProportyChanged()
        {
            StringUI _stringUI = new StringUI();

            PropertyChangedEventHandler _propertyChanged = (sender, e) => Assert.AreEqual("Buffer", e.PropertyName);

            _stringUI.PropertyChanged += _propertyChanged;

            object[] Array = new[] { "1", "2" };

            _stringUI.writeLine("Hello {0}", Array);

            Assert.AreEqual("* Hello 1", _stringUI.Buffer);
        }

    }
}

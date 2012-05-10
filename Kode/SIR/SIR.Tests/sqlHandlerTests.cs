using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using SqlInteraction;


namespace SIR.Tests
{

    [TestFixture]
    public class sqlHandlerTests
    {

        [Test]
        public void SetSingletonSqlHandling()
        {
            SQLHandler handler = SQLHandler.GetInstance;

            Assert.IsNotNull(handler);
        }

        [Test]
        public void ChangeConnectionstring()
        {
            SQLHandler handler = SQLHandler.GetInstance;
            handler.setConnection("hdjdk", "uhuuh", "bbuiiu", "Bbibiuiu", "30");

            Assert.AreNotEqual("dcfvgbhnjm", handler.connection.ConnectionString);
        }

        [Test]
        public void SetSingletonSqlHandlingConnectionstring()
        {
            SQLHandler handler = SQLHandler.GetInstance;

            Assert.AreEqual("Data Source=webhotel10.iha.dk;Initial Catalog=F12I4PRJ4Gr3;Persist Security Info=True;User " +
                            "ID=F12I4PRJ4Gr3;Password=F12I4PRJ4Gr3", handler.connection.ConnectionString);
        }


    }
}

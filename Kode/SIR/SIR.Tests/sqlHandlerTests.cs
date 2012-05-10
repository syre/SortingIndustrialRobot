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

        private bool connection; 

        [Test]
        public void SetSingletonSqlHandling()
        {
            ISQLHandler handler = SQLHandler.GetInstance;

            Assert.IsNotNull(handler);
        }
/*
        [Test]
        public void ChangeConnectionstring()
        {
            ISQLHandler handler = SQLHandler.GetInstance;

            connection = handler.setConnection("hdjdk", "uhuuh", "bbuiiu", "Bbibiuiu", "30");


            Assert.AreNotEqual("dcfvgbhnjm",);
        }

        [Test]
        public void SetSingletonSqlHandlingConnectionstring()
        {
            ISQLHandler handler = SQLHandler.GetInstance;

            Assert.AreEqual("Data Source=webhotel10.iha.dk;Initial Catalog=F12I4PRJ4Gr3;Persist Security Info=True;User " +
                            "ID=F12I4PRJ4Gr3;Password=F12I4PRJ4Gr3", handler);
        }
*/
    }
}

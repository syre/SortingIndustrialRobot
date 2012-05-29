using System.Data.SqlClient;
using NUnit.Framework;
using Rhino.Mocks;
using SqlInteraction;


namespace SIR.Tests
{

    [TestFixture]
    public class sqlHandlerTests
    {
        [Test]
        public void SetSingletonSqlHandlingShouldNotBeNull()
        {
            var handler = SQLHandler.GetInstance;
            Assert.IsNotNull(handler);
        }

        [Test]
        public void SetConnectionStringToAnotherConnection()
        {
            ISQLHandler handler = SQLHandler.GetInstance;
            handler.Connection = MockRepository.GenerateMock<ISqlConnection>();
            handler.setConnection("Minserver", "mindatabase", "yusuf", "michael", "30");
            handler.Connection.AssertWasCalled(s => s.Connectionstring = "Data Source=Minserver;" +
                                                                         "Initial Catalog=mindatabase;User ID=yusuf;" +                                                                      
                                                                         "Password=michael;Connection Timeout=30;");
        }

        [Test]
        public void CheckIfCommandReturned()
        {
            ISQLHandler handler = SQLHandler.GetInstance;
            handler.Connection  = MockRepository.GenerateStub<ISqlConnection>();
            handler.Connection.Stub(t => t.CreateCommand()).Return(new SqlCommand());

            SqlCommand command = handler.Connection.CreateCommand();
            Assert.IsNotNull(command);
        }


        [Test]
        public void AddParameterTest()
        {
            ISQLHandler handler = SQLHandler.GetInstance;
            handler.Connection = MockRepository.GenerateStub<ISqlConnection>();

        }



    }
}

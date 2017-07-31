using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace FCore.Tests.ControllerTests
{
    [TestClass]
    public class SqlTests
    {
        [TestMethod]
        public void ReturnListOfString_FromProcedure_WithScalarFunction_UsingDapper()
        {
            // set 
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FamilyContext"].ConnectionString);


            // act
            conn.Query

            // assert
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseaccesLayer;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Tests
{
    [TestClass]
    public class DBConnectionTests
    {
        [TestMethod]
        public void DatabaseConnectionOpenTest()
        {
            string connectionString = "Data Source = kraka.ucn.dk; Initial Catalog = dmab0915_2Sem_4; Persist Security Info = True; User ID = dmab0915_2Sem_4; Password = IsAllowed";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Assert.AreEqual(ConnectionState.Open, connection.State);
        }

        [TestMethod]
        public void DatabaseConnectionClosedTest()
        {
            string connectionString = "Data Source = kraka.ucn.dk; Initial Catalog = dmab0915_2Sem_4; Persist Security Info = True; User ID = dmab0915_2Sem_4; Password = IsAllowed";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            connection.Close();
            Assert.AreEqual(ConnectionState.Closed, connection.State);
        }
        
    }
}

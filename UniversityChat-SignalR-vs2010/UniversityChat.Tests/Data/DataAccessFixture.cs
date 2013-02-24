using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UniversityChat.Data.DataAccess;
using System.Data.Common;
using System.Data;
namespace UniversityChat.Tests.Data
{
    [TestFixture]
    public class DataAccessFixture
    {
        [Test]
        public void Validate_Database_Access()
        {
            DbCommand dbCommand = GenericDataAccess.CreateCommand(@"SELECT 1 FROM Test", null);
            DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);
            foreach (DataRow row in dataTable.Rows)
            {
                var r = row;
            }
        }
    }
}

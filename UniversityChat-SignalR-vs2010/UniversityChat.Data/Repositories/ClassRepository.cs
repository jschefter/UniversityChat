using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityChat.Model;
using System.Data.Common;
using UniversityChat.Data.DataAccess;
using System.Data;
using UniversityChat.Data.DataHelpers;

namespace UniversityChat.Data.Repositories
{
    public class ClassRepository : IRepository<Class>
    {
        public bool Create(Class item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Class item)
        {
            throw new NotImplementedException();
        }

        public IList<Class> GetAll()
        {
            throw new NotImplementedException();
        }

        public Guid GetDefaultClassGuid()
        {
            try
            {
                Guid defaultClassGuid = Guid.Empty;

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = ClassQueries.SellectAllClassesQuery;

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                if (dataTable != null)
                {
                    DataRow defaultRow = dataTable.Rows[0];

                    defaultClassGuid = Guid.Parse(defaultRow["ClassId"].ToString());
                }
                return defaultClassGuid;
            }
            catch (Exception exp)
            {
                return Guid.Empty;
            }
        }

        public Class GetById(decimal id)
        {
            throw new NotImplementedException();
        }

        public Class GetByCriteria(string criteriaName, Class item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Class item)
        {
            throw new NotImplementedException();
        }
    }
}

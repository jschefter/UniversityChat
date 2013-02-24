using System.Data;
using System.Data.Common;
using UniversityChat.Data.DataAccess;

namespace UniversityChat.Data.DataHelpers
{
    public class UserDataHelper
    {
        public static bool IsUserEmailExist(string eamil)
        {
            DbCommand comm = GenericDataAccess.CreateCommand();
            comm.CommandType = CommandType.Text;

            DbParameter emailParam = comm.CreateParameter();
            emailParam.ParameterName = "@eamil";
            emailParam.DbType = DbType.String;
            emailParam.Value = eamil;

            comm.Parameters.Add(emailParam);
            comm.CommandText = @"SELECT 1 FROM [UniversityChat].[Users] AS T1 WHERE T1.Email = @eamil";

            string returnedResult = GenericDataAccess.ExecuteStringScalarCommand(comm);

            if(string.IsNullOrEmpty(returnedResult))
            {
                return false;
            }
            return bool.Parse(returnedResult);
        }

        public static bool IsUserNickNameExist(string nickName)
        {
            DbCommand comm = GenericDataAccess.CreateCommand();
            comm.CommandType = CommandType.Text;

            DbParameter emailParam = comm.CreateParameter();
            emailParam.ParameterName = "@nickName";
            emailParam.DbType = DbType.String;
            emailParam.Value = nickName;

            comm.Parameters.Add(emailParam);
            comm.CommandText = @"SELECT 1 FROM [UniversityChat].[Users] AS T1 WHERE T1.NickName = @nickName";

            string returnedResult = GenericDataAccess.ExecuteStringScalarCommand(comm);

            if (string.IsNullOrEmpty(returnedResult))
            {
                return false;
            }
            return bool.Parse(returnedResult);
        }
    }
}

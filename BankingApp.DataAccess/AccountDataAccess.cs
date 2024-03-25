using BankingApp.Common;
using BankingApp.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.DataAcesss
{
    public class AccountDataAccess:BaseDataAccess
    {
        public AccountInfo FindAccountBy(int accountId)
        {
            AccountInfo acc=null;
            acc=new AccountInfo(1,"a",AccountType.Savings,1);
            string sql = "sp_GetAccountDetails";
            try
            {
                var reader = ExecuteReader(
                    sqltext: sql,
                    commandType: CommandType.StoredProcedure,
                    parameters: new SqlParameter("@AccountId", accountId)
                    );
                while (reader.Read()) {
                    acc = new AccountInfo
                    (
                        reader.GetInt32(0),
                        reader.GetString(1),
                        ("Savings" == reader.GetString(2)) ? AccountType.Savings : AccountType.Current,
                      Convert.ToDouble(reader.GetDecimal(3))
                    );
                }
                if (!reader.IsClosed) reader.Close();

            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return acc;
        }

    }
}

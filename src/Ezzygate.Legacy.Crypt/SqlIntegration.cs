using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace Ezzygate.Legacy.Crypt
{
    public static class Sql
    {
        [SqlFunction(DataAccess = DataAccessKind.None)]
        public static SqlBinary Encrypt(SqlInt32 sqlnKeyId, SqlString sqlsText)
        {
            return (SqlBinary)SymEncryption.GetKey((int)sqlnKeyId).Encrypt(new Blob(sqlsText.ToString())).Bytes;
        }

        [SqlFunction(DataAccess = DataAccessKind.None)]
        public static SqlString Decrypt(SqlInt32 sqlnKeyId, SqlBinary sqlbBinary)
        {
            return (SqlString)SymEncryption.GetKey((int)sqlnKeyId).Decrypt(new Blob((byte[])sqlbBinary)).Text;
        }
    }
}
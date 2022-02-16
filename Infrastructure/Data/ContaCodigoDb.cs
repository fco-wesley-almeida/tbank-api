using System;
using Core.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Data
{
    public class ContaCodigoDb: DbBase, IContaCodigoDb
    {
        protected ContaCodigoDb(IConfiguration configuration) : base(configuration)
        {
        }
        public int FindLastContaCodigo()
        {
            NpgsqlConnection conn;
            int codigo;
            const string sql = @"
                SELECT
                    CAST(codigo AS int)
                FROM conta
                ORDER BY codigo DESC
                LIMIT 1
            ";
            conn = GetConnection();
            try
            {
                codigo = conn.QueryFirst<int>(sql);
            }
            catch (InvalidOperationException)
            {
                codigo = 0;
            }
            conn.Close();
            return codigo;
        }

    }
}
using System;
using Core.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Data
{
    public class ContaSaldoDb: DbBase, IContaSaldoDb
    {
        public ContaSaldoDb(IConfiguration configuration) : base(configuration)
        {
        }

        public float FindSaldoConta(long contaId)
        {
             NpgsqlConnection conn;
             float saldo;
             const string sql = @"
                select
                    (saldo::numeric::float)
                from contas_saldo_mat_view
                where
                    conta_id = @ContaId
             ";
             conn = GetConnection();
             try
             {
                 saldo = conn.QueryFirst<float>(sql, new {ContaId = contaId});
             }
             catch (InvalidOperationException)
             {
                 saldo = 0;
             }
             conn.Close();
             return saldo;
        }
    }
}
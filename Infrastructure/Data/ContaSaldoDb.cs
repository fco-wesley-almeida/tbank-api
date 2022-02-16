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
                SELECT
                    (
                        COALESCE((
                            SELECT
                                SUM(t.valor)
                            FROM transacao_receita tr
                            INNER JOIN transacao t on t.id = tr.transacao_id
                            WHERE t.conta_id = c.id
                        ), 0::money)
                        -
                        COALESCE((
                            SELECT
                                sum(t.valor)
                            FROM transacao_debito td
                            INNER JOIN transacao t on t.id = td.transacao_id
                            WHERE t.conta_id = c.id
                        ), 0::money)
                    ) as saldo FROM conta c
                where
                    c.id = @ContaId
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

        public float FindLimiteDisponivel(long contaId)
        {
            NpgsqlConnection conn;
            float saldo;
            const string sql = @"
                SELECT limite_disponivel from conta where id = @ContaId
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
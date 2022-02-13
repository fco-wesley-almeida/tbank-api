using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Data
{
    public class DbBase
    {
        private readonly string _connectionString;

        protected NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString) ;
        }

        protected DbBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TBankContext");
        }
    }
}
using System;
using System.Collections.Generic;
using Core.Data;
using Core.Domains.Autenticacao.Dtos;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Data
{
    public class LoginDb: DbBase, ILoginDb
    {
        public LoginDb(IConfiguration configuration) : base(configuration) {}

        public UsuarioParaAutenticarDto FindUsuarioParaAutenticar(LoginRequestDto request)
        {
            NpgsqlConnection conn;
            UsuarioParaAutenticarDto usuario;
            const string sql = @"
                SELECT
                    u.senha as Senha,
                    u.id as UsuarioId
                FROM usuario u
                INNER JOIN pessoa p ON p.id = u.pessoa_id
                WHERE
                    p.email = @Email
            ";
            conn = GetConnection();
            try
            {
                usuario = conn.QueryFirst<UsuarioParaAutenticarDto>(sql, request);
            }
            catch (InvalidOperationException)
            {
                usuario = new UsuarioParaAutenticarDto {UsuarioId = 0};
            }
            conn.Close();
            return usuario;
        }
    }
}
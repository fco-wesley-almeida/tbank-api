using Business.CoreServices;
using Business.Services.Autenticacao;
using Business.Services.Contas;
using Business.Validators;
using Core.CoreServices;
using Core.Data;
using Core.Domains.Autenticacao.Services;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
using Core.Entities;
using Core.Repositories;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterService(IServiceCollection services)
        {
            ConfigureRepositories(services);
            services.AddScoped<IPasswordEncoder, PasswordEncoder>();
            services.AddScoped<IContaCodigoDb, ContaCodigoDb>();
            services.AddScoped<ILoginDb, LoginDb>();
            services.AddTransient<IValidator<ContaPessoaFisicaCadastroDto>, ContaPessoaFisicaCadastroValidator>();
            services.AddTransient<IValidator<ContaPessoaJuridicaCadastroDto>, ContaPessoaJuridicaCadastroValidator>();
            services.AddScoped<ICalculoDadosCadastroProximaContaService, CalculoDadosCadastroProximaContaService>();
            services.AddScoped<IContaCadastroPessoaFisicaService, ContaCadastroPessoaFisicaService>();
            services.AddScoped<IContaCadastroPessoaJuridicaService, ContaCadastroPessoaJuridicaService>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IAgenciaRepository, AgenciaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IPessoaFisicaRepository, PessoaFisicaRepository>();
            services.AddScoped<IPessoaJuridicaRepository, PessoaJuridicaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
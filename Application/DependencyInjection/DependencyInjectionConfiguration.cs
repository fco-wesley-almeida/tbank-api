using Business.Services.Autenticacao;
using Business.Services.Contas;
using Business.Validators;
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
            services.AddScoped<IContaCodigoDb, ContaCodigoDb>();
            services.AddScoped<ILoginDb, LoginDb>();
            services.AddTransient<IValidator<ContaPessoaFisicaCadastroDto>, ContaPessoaFisicaCadastroValidator>();
            services.AddScoped<ICalculoDadosCadastroProximaContaService, CalculoDadosCadastroProximaContaService>();
            services.AddScoped<IContaCadastroPessoaFisicaService, ContaCadastroPessoaFisicaService>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            // services.AddScoped<IRepository<Conta>, RepositoryBase<Conta>>();
            services.AddScoped<IContaRepository, ContaRepository>();
            // services.AddScoped<IRepository<Agencia>, RepositoryBase<Agencia>>();
            // services.AddScoped<IRepository<Endereco>, RepositoryBase<Endereco>>();
            // services.AddScoped<IRepository<Pessoa>, RepositoryBase<Pessoa>>();
            // services.AddScoped<IRepository<PessoaEndereco>, RepositoryBase<PessoaEndereco>>();
            // services.AddScoped<IRepository<PessoaFisica>, RepositoryBase<PessoaFisica>>();
            // services.AddScoped<IRepository<PessoaJuridica>, RepositoryBase<PessoaJuridica>>();
            // services.AddScoped<IRepository<Usuario>, RepositoryBase<Usuario>>();
        }
    }
}
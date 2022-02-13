using System.Threading.Tasks;
using Core.Domains.Contas.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Core.Domains.Contas.Services
{
    public interface IContaCadastroPessoaFisicaService
    {
        public Task<long> Cadastrar(ContaPessoaFisicaCadastroDto request);
    }
}
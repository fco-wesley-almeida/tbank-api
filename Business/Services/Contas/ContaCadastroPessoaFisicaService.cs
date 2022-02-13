using System.Threading.Tasks;
using Core.Domains.Contas.Dtos;
using Core.Domains.Contas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Business.Services.Contas
{
    public class ContaCadastroPessoaFisicaService: IContaCadastroPessoaFisicaService
    {
        public async Task<long> Cadastrar(ContaPessoaFisicaCadastroDto request)
        {
            return 1;
        }
    }
}
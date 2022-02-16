using Core.Domains.Contas.Dtos;

namespace Core.Domains.Contas.Services
{
    public interface IContaCadastroPessoaJuridicaService
    {
        public long Cadastrar(ContaPessoaJuridicaCadastroDto request);
    }
}
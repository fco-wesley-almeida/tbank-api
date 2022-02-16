using Core.Entities;

namespace Core.Repositories
{
    public interface IPessoaJuridicaRepository: IRepository<PessoaJuridica>
    {
        bool CnpjJaExiste(string cnpj);
    }
}
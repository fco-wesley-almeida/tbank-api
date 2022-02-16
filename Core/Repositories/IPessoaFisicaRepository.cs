using Core.Entities;

namespace Core.Repositories
{
    public interface IPessoaFisicaRepository: IRepository<PessoaFisica>
    {
        bool CpfJaExiste(string cpf);
    }
}
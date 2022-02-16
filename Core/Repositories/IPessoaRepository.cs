using Core.Entities;

namespace Core.Repositories
{
    public interface IPessoaRepository: IRepository<Pessoa>
    {
        bool EmailJaExiste(string email);
    }
}
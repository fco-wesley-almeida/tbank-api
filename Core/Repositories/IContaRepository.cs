using Core.Entities;

namespace Core.Repositories
{
    public interface IContaRepository: IRepository<Conta>
    {
        Conta FindById(long contaId);
    }
}
using Core.Entities;

namespace Core.Repositories
{
    public interface IFaturaRepository: IRepository<Fatura>
    {
        Fatura FindByContaId(long contaId);
    }
}
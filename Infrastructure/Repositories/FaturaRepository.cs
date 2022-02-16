using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class FaturaRepository: RepositoryBase<Fatura>, IFaturaRepository
    {
        public FaturaRepository(TBankDbContext context) : base(context)
        {
        }

        public Fatura FindByContaId(long contaId)
        {
            IQueryable<Fatura> query = from fatura in GetDbContext().Faturas
                where fatura.ContaId == (int)contaId
                select fatura;
            return query.FirstOrDefault();
        }
    }
}
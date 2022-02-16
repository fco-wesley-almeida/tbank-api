using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class FaturaRepository: RepositoryBase<Fatura>, IFaturaRepository
    {
        public FaturaRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
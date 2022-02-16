using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class PagamentoFaturaRepository: RepositoryBase<PagamentoFatura>, IPagamentoFaturaRepository
    {
        public PagamentoFaturaRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
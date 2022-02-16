using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class TransacaoDebitoRepository: RepositoryBase<TransacaoDebito>, ITransacaoDebitoRepository
    {
        public TransacaoDebitoRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class TransacaoReceitaRepository: RepositoryBase<TransacaoReceita>, ITransacaoReceitaRepository
    {
        public TransacaoReceitaRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
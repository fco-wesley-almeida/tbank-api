using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class TransacaoRepository: RepositoryBase<Transacao>, ITransacaoRepository
    {
        public TransacaoRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
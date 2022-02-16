using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class TransacaoCreditoRepository: RepositoryBase<TransacaoCredito>, ITransacaoCreditoRepository
    {
        public TransacaoCreditoRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
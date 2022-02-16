using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class ContaRepository: RepositoryBase<Conta>, IContaRepository
    {
        public ContaRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class ContaRepository: RepositoryBase<Conta>, IContaRepository
    {
        public ContaRepository(TBankDbContext context) : base(context)
        {
        }

        public Conta FindById(long contaId)
        {
            IQueryable<Conta> query = from conta in GetDbContext().Conta
                where conta.Id == contaId
                select conta;
            return query.FirstOrDefault();
        }
    }
}
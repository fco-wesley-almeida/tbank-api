using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class AgenciaRepository: RepositoryBase<Agencia>, IAgenciaRepository
    {
        public AgenciaRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
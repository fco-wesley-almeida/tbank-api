using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class PessoaRepository: RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
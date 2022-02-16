using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class PessoaJuridicaRepository: RepositoryBase<PessoaJuridica>, IPessoaJuridicaRepository
    {
        public PessoaJuridicaRepository(TBankDbContext context) : base(context)
        {
        }

    }
}
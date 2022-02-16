using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class PessoaFisicaRepository: RepositoryBase<PessoaFisica>, IPessoaFisicaRepository
    {
        public PessoaFisicaRepository(TBankDbContext context) : base(context)
        {
        }

        public bool CpfJaExiste(string cpf)
        {
            return GetDbContext().PessoaFisicas.Any(p => p.Cpf == cpf);
        }
    }
}
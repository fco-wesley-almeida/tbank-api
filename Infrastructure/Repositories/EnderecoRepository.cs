using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class EnderecoRepository: RepositoryBase<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
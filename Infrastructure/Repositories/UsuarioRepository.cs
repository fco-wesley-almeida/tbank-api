using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository: RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TBankDbContext context) : base(context)
        {
        }
    }
}
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.Repositories
{
    public interface IRepository<in T> where T: class
    {
        public void Create(T entity);
        public void Update(T entity);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
    }
}
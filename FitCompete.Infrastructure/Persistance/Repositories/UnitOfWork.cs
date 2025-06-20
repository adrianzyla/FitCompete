using FitCompete.Domain.Interfaces;
using FitCompete.Infrastructure.Persistence.Repositories;
using System.Collections;

namespace FitCompete.Infrastructure.Persistence
{
    // Nazwa folderu może być inna, ważne, żeby zawartość klasy się zgadzała
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Hashtable _repositories = new Hashtable();

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

                if (repositoryInstance != null)
                {
                    _repositories.Add(type, repositoryInstance);
                }
                else
                {
                    throw new InvalidOperationException($"Could not create a repository instance for type {type}");
                }
            }

            return (IGenericRepository<T>)_repositories[type]!;
        }
    }
}
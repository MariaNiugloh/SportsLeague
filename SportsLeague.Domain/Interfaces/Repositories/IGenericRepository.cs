using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : AuditBase // T significa que es un tipo genérico que debe heredar de AuditBase, pude que se convierta de tipo Team, Player, Match, etc.

    {
        // 6 méthodes génériques pour les opérations CRUD
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);

    }
}

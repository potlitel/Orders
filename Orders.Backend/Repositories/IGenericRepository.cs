using Orders.Shared.DTOs;

namespace Orders.Backend.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<Response<T>> GetAsync(int id);

        Task<Response<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

        Task<Response<T>> AddAsync(T entity);

        Task<Response<T>> DeleteAsync(int id);

        Task<Response<T>> UpdateAsync(T entity);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
using Orders.Shared.DTOs;

namespace Orders.Backend.Repositories
{
    public interface ICitiesRepository
    {
        Task<Response<IEnumerable<City>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
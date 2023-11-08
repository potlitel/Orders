using Orders.Shared.DTOs;

namespace Orders.Backend.Repositories
{
    public interface ICountriesRepository
    {
        Task<Response<Country>> GetAsync(int id);

        Task<Response<IEnumerable<Country>>> GetAsync();

        Task<Response<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
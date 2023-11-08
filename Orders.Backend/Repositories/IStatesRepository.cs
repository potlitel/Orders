using Orders.Shared.DTOs;

namespace Orders.Backend.Repositories
{
    public interface IStatesRepository
    {
        Task<Response<State>> GetAsync(int id);

        Task<Response<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
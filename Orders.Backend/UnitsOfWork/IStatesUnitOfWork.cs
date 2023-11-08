using Orders.Shared.DTOs;

namespace Orders.Backend.UnitsOfWork
{
    public interface IStatesUnitOfWork
    {
        Task<Response<State>> GetAsync(int id);

        Task<Response<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
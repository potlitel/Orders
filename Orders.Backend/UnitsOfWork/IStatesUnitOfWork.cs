namespace Orders.Backend.UnitsOfWork
{
    public interface IStatesUnitOfWork
    {
        Task<Response<State>> GetAsync(int id);

        Task<Response<IEnumerable<State>>> GetAsync();
    }
}
namespace Orders.Backend.UnitsOfWork
{
    public interface ICountriesUnitOfWork
    {
        Task<Response<Country>> GetAsync(int id);

        Task<Response<IEnumerable<Country>>> GetAsync();
    }
}
namespace Orders.Backend.Repositories
{
    public interface ICountriesRepository
    {
        Task<Response<Country>> GetAsync(int id);

        Task<Response<IEnumerable<Country>>> GetAsync();
    }
}
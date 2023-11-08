using Orders.Backend.Repositories;

namespace Orders.Backend.UnitsOfWork
{
    public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
        {
            _countriesRepository = countriesRepository;
        }

        public override async Task<Response<IEnumerable<Country>>> GetAsync() => await _countriesRepository.GetAsync();

        public override async Task<Response<Country>> GetAsync(int id) => await _countriesRepository.GetAsync(id);
    }
}
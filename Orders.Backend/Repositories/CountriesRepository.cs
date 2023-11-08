namespace Orders.Backend.Repositories
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;

        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .Include(c => c.States)
                .ToListAsync();
            return new Response<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }

        public override async Task<Response<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
                 .Include(c => c.States!)
                 .ThenInclude(s => s.Cities)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return new Response<Country>
                {
                    WasSuccess = false,
                    Message = "País no existe"
                };
            }

            return new Response<Country>
            {
                WasSuccess = true,
                Result = country
            };
        }
    }
}
namespace Orders.Backend.Repositories
{
    public class StatesRepository : GenericRepository<State>, IStatesRepository
    {
        private readonly DataContext _context;

        public StatesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<State>>> GetAsync()
        {
            var states = await _context.States
                .Include(s => s.Cities)
                .ToListAsync();
            return new Response<IEnumerable<State>>
            {
                WasSuccess = true,
                Result = states
            };
        }

        public override async Task<Response<State>> GetAsync(int id)
        {
            var state = await _context.States
                 .Include(s => s.Cities)
                 .FirstOrDefaultAsync(s => s.Id == id);

            if (state == null)
            {
                return new Response<State>
                {
                    WasSuccess = false,
                    Message = "Estado no existe"
                };
            }

            return new Response<State>
            {
                WasSuccess = true,
                Result = state
            };
        }
    }
}
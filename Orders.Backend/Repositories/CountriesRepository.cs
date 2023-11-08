﻿using Orders.Backend.Helpers;
using Orders.Shared.DTOs;

namespace Orders.Backend.Repositories
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;

        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            //if (!string.IsNullOrWhiteSpace(pagination.Filter))
            //{
            //    queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            //}

            return new Response<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(c => c.Name)
                    .Paginate(pagination)
                    .ToListAsync()
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

        public Task<Response<IEnumerable<Country>>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries.AsQueryable();

            //if (!string.IsNullOrWhiteSpace(pagination.Filter))
            //{
            //    queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            //}

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new Response<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}
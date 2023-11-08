﻿namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        public CitiesController(IGenericUnitOfWork<City> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
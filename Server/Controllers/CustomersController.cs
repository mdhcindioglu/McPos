using McPos.Server.Data;
using McPos.Shared;
using McPos.Shared.Models;
using McPos.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography.Xml;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using McPos.Server.Data.Models;
using static Humanizer.On;

namespace McPos.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly DataBase _db;
        private readonly IMapper _mapper;

        public CustomersController(ILogger<CustomersController> logger, DataBase db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll(int curpage, string? orderby, int skip, int take, string? search = null)
        {
            var response = new Response<PagedList<CustomerVM>>() { Data = new() { Items = new(), }, };

            try
            {
                var customers = _db.Customers.AsNoTracking();

                response.Data.AllItemCount = await customers?.CountAsync();

                search = search?.Trim();
                if (search.HasValue())
                    customers = customers?.Where(x => x.FullName.Contains(search));

                response.Data.SelectedCount = await customers?.CountAsync();
                
                customers = customers.OrderBy(orderby);
                skip = response.Data.Pagination(take, curpage, search);

                response.Data.Items = await customers.Skip(skip).Take(take).ProjectTo<CustomerVM>(_mapper.ConfigurationProvider).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed get all customers");
                response.AddError(ResponseCodes.Failed, "Failed get all customers");
            }

            return Ok(response);
        }
    }
}
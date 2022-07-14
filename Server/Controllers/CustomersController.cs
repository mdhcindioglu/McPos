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
        public async Task<ActionResult> GetAll(int curpage = 1, string? orderby = "Id", int skip = 0, int take = 25, string? search = null)
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

        [HttpGet("GetCustomer")]
        public async Task<ActionResult> GetCustomer(int id)
        {
            var response = new Response<CustomerVM>();

            try
            {
                response.Data = await _db.Customers.ProjectTo<CustomerVM>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
                if (response.Data == null)
                    response.AddError(ResponseCodes.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed get customer id = {id}");
                response.AddError(ResponseCodes.Failed, $"Failed get customer id = {id}");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CustomerVM customerVM)
        {
            var response = new Response<CustomerVM>();

            try
            {
                Customer customer = _mapper.Map<Customer>(customerVM);
                var entity = await _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();

                response.Data = _mapper.Map<CustomerVM>(entity.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed create customer");
                response.AddError(ResponseCodes.Failed, $"Failed create customer");
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(CustomerVM customerVM)
        {
            var response = new Response<CustomerVM>();

            try
            {
                Customer customer = _mapper.Map<Customer>(customerVM);
                _db.Customers.Update(customer);
                await _db.SaveChangesAsync();

                response.Data = customerVM;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed update customer");
                response.AddError(ResponseCodes.Failed, $"Failed update customer");
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var response = new Response();

            try
            {
                var customer = await _db.Customers.FirstOrDefaultAsync(x=>x.Id == id);
                if(customer != null)
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed delete customer");
                response.AddError(ResponseCodes.Failed, $"Failed delete customer");
            }

            return Ok(response);
        }

        [HttpGet("IsFullNameAdded")]
        public async Task<ActionResult> IsFullNameAdded(string FullName, int Id = 0)
        {
            var response = new Response<bool>();

            try
            {
                response.Data = await _db.Customers.AnyAsync(x=>x.FullName == FullName && x.Id != Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed check customer full name exists.");
                response.AddError(ResponseCodes.Failed, "Failed check customer full name exists.");
            }

            return Ok(response);
        }
    }
}
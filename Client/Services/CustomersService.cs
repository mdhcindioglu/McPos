using McPos.Shared.Extenstions;
using McPos.Shared.Models;
using McPos.Shared.ViewModels;
using System.Net.Http.Json;

namespace McPos.Client.Services
{
    public class CustomersService: ICustomersService
    {
        private readonly HttpClient _http;

        public CustomersService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Response<PagedList<CustomerVM>>> GetCustomers(Request request) =>
            await _http.GetDataFromUrlAsync<Response<PagedList<CustomerVM>>>($"/Api/Customers/GetAll?search={request.Search.Trim()}&orderby={request.OrderBy}&skip={request.Skip}&take={request.Take}&curpage={request.CurPage}");
    }

    public interface ICustomersService
    {
        Task<Response<PagedList<CustomerVM>>> GetCustomers(Request request);
    }
}

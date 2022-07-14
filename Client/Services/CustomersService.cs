using McPos.Shared.Extenstions;
using McPos.Shared.Models;
using McPos.Shared.ViewModels;
using System.Net.Http.Json;

namespace McPos.Client.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _http;

        public CustomerService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Response<PagedList<CustomerVM>>?> GetCustomers(Request request) =>
            await _http.GetFromJsonAsync<Response<PagedList<CustomerVM>>?>($"/Api/Customers/GetAll?search={request.Search.Trim()}&orderby={request.OrderBy}&skip={request.Skip}&take={request.Take}&curpage={request.CurPage}");

        public async Task<Response<CustomerVM>?> GetCustomer(int id) =>
            await _http.GetFromJsonAsync<Response<CustomerVM>?>($"/Api/Customers/GetCustomer?id={id}");

        public async Task<Response<CustomerVM>> CreateCustomer(CustomerVM customerVM) =>
            await _http.PostDataFromUrlAsync<Response<CustomerVM>>($"/Api/Customers", customerVM);

        public async Task<Response<CustomerVM>> UpdateCustomer(CustomerVM customerVM) =>
            await _http.PutDataFromUrlAsync<Response<CustomerVM>>($"/Api/Customers", customerVM);

        public async Task<Response> DeleteCustomer(int id) =>
            await _http.DeleteDataFromUrlAsync<Response>($"/Api/Customers?id={id}");

        public async Task<Response<bool>?> IsFullNameAdded(string FullName, int Id = 0) =>
            await _http.GetFromJsonAsync<Response<bool>?>($"/Api/Customers/IsFullNameAdded?fullname={FullName.Trim()}&id={Id}");
    }

    public interface ICustomerService
    {
        Task<Response<PagedList<CustomerVM>>> GetCustomers(Request request);
        Task<Response<CustomerVM>> GetCustomer(int id);
        Task<Response<CustomerVM>> CreateCustomer(CustomerVM customerVM);
        Task<Response<CustomerVM>> UpdateCustomer(CustomerVM customerVM);
        Task<Response> DeleteCustomer(int id);
        Task<Response<bool>> IsFullNameAdded(string FullName, int Id = 0);
    }
}

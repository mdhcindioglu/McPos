using Blazored.Modal;
using Blazored.Modal.Services;
using McPos.Client.Services;
using McPos.Shared.Models;
using McPos.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace McPos.Client.Pages.Customers
{
    public partial class Index
    {
        [Inject] public ICustomerService CustomersSrv { get; set; }
        [Inject] public IModalService ModalSrv { get; set; }

        Request request = new ();
        PagedList<CustomerVM> response = new();

        protected override async Task OnInitializedAsync()
        {
            await FillCustomers();
        }

        async Task FillCustomers()
        {
            Response<PagedList<CustomerVM>> responseCustomers = await CustomersSrv.GetCustomers(request);
            if (responseCustomers.IsSuccess)
                response = responseCustomers.Data;
        }

        async Task OpenCustomerDialog(CustomerVM customer)
        {
            var title = customer.Id == 0 ? "New customer" : "Edit Customer";
            ModalParameters modalParameters = new();
            modalParameters.Add("Customer", customer);
            var modal = ModalSrv.Show<CustomerDialog>(title, modalParameters, new ModalOptions() { HideCloseButton = true, });
            var result = await modal.Result;
            if (!result.Cancelled)
            {
                customer = result.Data as CustomerVM;
                await FillCustomers();
            }
        }
    }
}

using Blazored.Modal;
using Blazored.Modal.Services;
using McPos.Client.Services;
using McPos.Client.Extensions;
using McPos.Shared.Models;
using McPos.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace McPos.Client.Pages.Customers
{
    public partial class Index
    {
        [Inject] public ICustomerService CustomersSrv { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        [Inject] public IModalService ModalSrv { get; set; }
        [Inject] public IToastService ToastSrv { get; set; }

        Request request = new();
        PagedList<CustomerVM> response = new();

        protected override async Task OnInitializedAsync()
        {
            await FillCustomers();
        }

        async Task FillCustomers()
        {
            var responseCustomers = await CustomersSrv.GetCustomers(request);
            if (responseCustomers.IsSuccess)
                response = responseCustomers.Data;
        }

        async Task OpenCustomerDialog(CustomerVM customer)
        {
            var title = customer.Id == 0 ? "New customer" : "Edit Customer";
            CustomerVM customerInDb = null;
            if (customer.Id == 0)
                customerInDb = new CustomerVM();
            else
            {
                var response = await CustomersSrv.GetCustomer(customer.Id);
                if (response.IsSuccess)
                    customerInDb = response.Data;
                else
                {
                    ToastSrv.ShowError("This customer has been deleted.");
                    await FillCustomers();
                    return;
                }
            }
            ModalParameters modalParameters = new();
            modalParameters.Add("Customer", customerInDb);
            var modal = ModalSrv.Show<CustomerDialog>(title, modalParameters, new ModalOptions() { HideCloseButton = true, });
            var result = await modal.Result;
            if (!result.Cancelled)
            {
                customer = result.Data as CustomerVM;
                await FillCustomers();
            }
        }

        async Task Delete(int id)
        {
            if (await JS.ShowConfirmation("Are you sure to delete this customer?"))
            {
                var responseDelete = await CustomersSrv.DeleteCustomer(id);
                if (responseDelete.IsSuccess)
                {
                    ToastSrv.ShowSuccess("Customer deleted.");
                    await FillCustomers();
                }
                else
                    ToastSrv.ShowError("Deleting customer failed.");
            }
        }
    }
}

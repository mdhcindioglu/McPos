using Blazored.Modal;
using Blazored.Modal.Services;
using McPos.Client.Extensions;
using McPos.Client.Services;
using McPos.Client.Shared;
using McPos.Shared.Models;
using McPos.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace McPos.Client.Pages.Customers
{
    public partial class CustomerDialog
    {
        [CascadingParameter] BlazoredModalInstance? ModalInstance { get; set; }
        [Inject] public ICustomerService? CustomerSrv { get; set; }
        [Inject] public IToastService? ToastSrv { get; set; }
        [Inject] public IJSRuntime? JS { get; set; }
        [Parameter] public CustomerVM? Customer { get; set; } = new();
        CsValidation? CsValidation { get; set; }
        bool Saving { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender) { if (firstRender) await JS.SetFocus(); }

        async void OnSave()
        {
            Saving = true;

            var errors = new Dictionary<string, List<string>>();

            var response = await CustomerSrv.IsFullNameAdded(Customer.FullName, Customer.Id);
            if (response.IsSuccess)
            {
                if (response.Data)
                    errors.Add(nameof(Customer.FullName), new() { "This name is exists." });
            }
            else
                errors.Add(nameof(Customer.FullName), new() { response.Error.Message });

            if (errors.Any())
                CsValidation.DisplayErrors(errors);
            else
            {
                Response<CustomerVM> responseCustomer;
                if (Customer.Id == 0)
                    responseCustomer = await CustomerSrv.CreateCustomer(Customer);
                else
                    responseCustomer = await CustomerSrv.UpdateCustomer(Customer);

                if (responseCustomer.IsSuccess)
                {
                    await ModalInstance.CloseAsync(ModalResult.Ok(response.Data));
                }
                else
                    ToastSrv.ShowError($"Failed to {(Customer.Id == 0 ? "Adding" : "Updating")} customer.");
            }

            Saving = false;
            StateHasChanged();
        }

        async void CloseDialog()
        {
            await ModalInstance.CloseAsync(ModalResult.Cancel());
        }
    }
}

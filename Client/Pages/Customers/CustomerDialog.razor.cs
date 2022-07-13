using McPos.Client.Shared;
using McPos.Shared.ViewModels;
using Microsoft.AspNetCore.Components;

namespace McPos.Client.Pages.Customers
{
    public partial class CustomerDialog
    {
        [Parameter] public CustomerVM? Customer { get; set; } = new();
        CsValidation? CsValidation { get; set; }
        bool Saving { get; set; }

        void OnSave()
        {

        } 
    }
}

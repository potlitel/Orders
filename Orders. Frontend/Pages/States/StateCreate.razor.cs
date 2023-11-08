using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.States
{
    public partial class StateCreate
    {
        private State state = new();
        private StateForm? stateForm;

        [Parameter]
        public int CountryId { get; set; }

        private async Task CreateAsync()
        {
            state.CountryId = CountryId;
            var response = await repository.PostAsync("/api/states", state);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            stateForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo($"/countries/details/{CountryId}");
        }
    }
}
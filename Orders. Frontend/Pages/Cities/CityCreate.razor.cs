using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.Cities
{
    public partial class CityCreate
    {
        private City city = new();
        private CityForm? cityForm;

        [Parameter]
        public int StateId { get; set; }

        private async Task CreateAsync()
        {
            city.StateId = StateId;
            var response = await repository.PostAsync("/api/cities", city);
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
            cityForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo($"/states/details/{StateId}");
        }
    }
}
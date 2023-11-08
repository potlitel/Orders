using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Shared.Entities;
using System.Net;

namespace Orders._Frontend.Pages.States
{
    public partial class StateEdit
    {
        private State? state;
        private StateForm? stateForm;

        [Parameter]
        public int StateId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var response = await repository.GetAsync<State>($"/api/states/{StateId}");
            if (response.Error)
            {
                if (response.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            state = response.Response;
        }

        private async Task SaveAsync()
        {
            var response = await repository.PutAsync($"/api/states", state);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            stateForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo($"/countries/details/{state!.CountryId}");
        }
    }
}
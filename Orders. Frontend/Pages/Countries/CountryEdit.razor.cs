using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.Countries
{
    public partial class CountryEdit
    {
        private Country? country;
        private CountryForm? countryForm;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHTTP = await repository.GetAsync<Country>($"api/countries/{Id}");

            if (responseHTTP.Error)
            {
                if (responseHTTP.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("countries");
                }
                else
                {
                    var messageError = await responseHTTP.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                country = responseHTTP.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHTTP = await repository.PutAsync("api/countries", country);

            if (responseHTTP.Error)
            {
                var mensajeError = await responseHTTP.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
            }
            else
            {
                countryForm!.FormPostedSuccessfully = true;
                navigationManager.NavigateTo("countries");
            }
        }

        private void Return()
        {
            navigationManager.NavigateTo("countries");
        }
    }
}
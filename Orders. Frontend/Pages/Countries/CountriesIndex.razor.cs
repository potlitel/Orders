using CurrieTechnologies.Razor.SweetAlert2;
using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        public List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        private async Task Load()
        {
            var responseHppt = await repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHppt.Response!;
        }

        private async Task DeleteAsync(Country country)
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Esta seguro que quieres borrar el país: {country.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHTTP = await repository.DeleteAsync($"api/countries/{country.Id}");

            if (responseHTTP.Error)
            {
                if (responseHTTP.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    var mensajeError = await responseHTTP.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
            }
            else
            {
                await Load();
            }
        }
    }
}
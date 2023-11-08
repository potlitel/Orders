using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.Countries
{
    public partial class CountryCreate
    {
        private CountryForm? countryForm;
        private Country country = new();

        private async Task CreateAsync()
        {
            var responseHttp = await repository.PostAsync("/api/countries", country);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message);
                return;
            }
            else
            {
                countryForm!.FormPostedSuccessfully = true;
                navigationManager.NavigateTo("countries");
            }
        }

        private void Return()
        {
            navigationManager.NavigateTo("/countries");
        }
    }
}
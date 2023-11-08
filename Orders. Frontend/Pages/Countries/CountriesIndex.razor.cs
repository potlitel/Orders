using CurrieTechnologies.Razor.SweetAlert2;
using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        public List<Country>? Countries { get; set; }
        private int currentPage = 1;
        private int totalPages;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        //private async Task Load()
        //{
        //    var responseHppt = await repository.GetAsync<List<Country>>("api/countries");
        //    Countries = responseHppt.Response!;
        //}

        //private async Task DeleteAsync(Country country)
        //{
        //    var result = await sweetAlertService.FireAsync(new SweetAlertOptions
        //    {
        //        Title = "Confirmación",
        //        Text = $"¿Esta seguro que quieres borrar el país: {country.Name}?",
        //        Icon = SweetAlertIcon.Question,
        //        ShowCancelButton = true
        //    });

        //    var confirm = string.IsNullOrEmpty(result.Value);

        //    if (confirm)
        //    {
        //        return;
        //    }

        //    var responseHTTP = await repository.DeleteAsync($"api/countries/{country.Id}");

        //    if (responseHTTP.Error)
        //    {
        //        if (responseHTTP.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        {
        //            navigationManager.NavigateTo("/");
        //        }
        //        else
        //        {
        //            var mensajeError = await responseHTTP.GetErrorMessageAsync();
        //            await sweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
        //        }
        //    }
        //    else
        //    {
        //        await Load();
        //    }
        //}

        private async Task SelectedPageAsync(int page)
        {
            currentPage = page;
            await LoadAsync(page);
        }

        private async Task LoadAsync(int page = 1)
        {
            var ok = await LoadListAsync(page);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }

        private async Task<bool> LoadListAsync(int page)
        {
            var response = await repository.GetAsync<List<Country>>($"api/countries?page={page}");
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Countries = response.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var response = await repository.GetAsync<int>("api/countries/totalPages");
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = response.Response;
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

            var response = await repository.DeleteAsync($"api/countries/{country.Id}");
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoadAsync();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }
    }
}
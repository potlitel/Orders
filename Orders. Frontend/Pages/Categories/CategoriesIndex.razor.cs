using CurrieTechnologies.Razor.SweetAlert2;
using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.Categories
{
    public partial class CategoriesIndex
    {
        public List<Category>? Categories { get; set; }
        private int currentPage = 1;
        private int totalPages;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

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
            var response = await repository.GetAsync<List<Category>>($"api/categories?page={page}");
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Categories = response.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var response = await repository.GetAsync<int>("api/categories/totalPages");
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = response.Response;
        }

        private async Task Load()
        {
            var responseHppt = await repository.GetAsync<List<Category>>("api/categories");
            Categories = responseHppt.Response!;
        }

        private async Task DeleteAsync(Category category)
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Esta seguro que quieres borrar la categoría: {category.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHTTP = await repository.DeleteAsync($"api/categories/{category.Id}");

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
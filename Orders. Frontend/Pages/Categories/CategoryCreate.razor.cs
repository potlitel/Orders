using CurrieTechnologies.Razor.SweetAlert2;
using Orders.Shared.Entities;

namespace Orders._Frontend.Pages.Categories
{
    public partial class CategoryCreate
    {
        private Category category = new();
        private CategoryForm? categoryForm;

        private async Task CreateAsync()
        {
            var httpResponse = await repository.PostAsync("api/categories", category);

            if (httpResponse.Error)
            {
                var mensajeError = await httpResponse.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
            }
            else
            {
                categoryForm!.FormPostedSuccessfully = true;
                navigationManager.NavigateTo("categories");
            }
        }

        private void Return()
        {
            navigationManager.NavigateTo("categories");
        }
    }
}
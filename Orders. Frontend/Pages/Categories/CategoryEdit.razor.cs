using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Shared.Entities;
using System.Net;

namespace Orders._Frontend.Pages.Categories
{
    public partial class CategoryEdit
    {
        private Category? category;
        private CategoryForm? categoryForm;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHTTP = await repository.GetAsync<Category>($"api/categories/{Id}");

            if (responseHTTP.Error)
            {
                if (responseHTTP.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("categories");
                }
                else
                {
                    var messageError = await responseHTTP.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                category = responseHTTP.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHTTP = await repository.PutAsync("api/categories", category);

            if (responseHTTP.Error)
            {
                var mensajeError = await responseHTTP.GetErrorMessageAsync();
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
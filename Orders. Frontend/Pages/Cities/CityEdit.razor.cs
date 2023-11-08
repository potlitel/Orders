﻿using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Shared.Entities;
using System.Net;

namespace Orders._Frontend.Pages.Cities
{
    public partial class CityEdit
    {
        private City? city;
        private CityForm? cityForm;

        [Parameter]
        public int CityId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var response = await repository.GetAsync<City>($"/api/cities/{CityId}");
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
            city = response.Response;
        }

        private async Task SaveAsync()
        {
            var response = await repository.PutAsync($"/api/cities", city);
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
            cityForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo($"/states/details/{city!.StateId}");
        }
    }
}
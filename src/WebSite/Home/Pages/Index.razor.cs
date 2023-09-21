using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebSite.Basket.Components;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Home.Pages
{
    public partial class Index
    {

        

        ProductQuantityOutput productQuantityOutput = default!;

        public void OnProductQuantityUpdatedEventHandler(ProductQuantityEventArgs args)
        {
            productQuantityOutput.UpdateQuantity(args);
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();

            bool isAuthenticated = authState.User.Identity.IsAuthenticated;

            await base.OnInitializedAsync();
        }
    }



}

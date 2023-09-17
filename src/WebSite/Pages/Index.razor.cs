using Microsoft.AspNetCore.Components;
using WebSite.Basket.Components;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Pages
{
    public partial class Index
    {
        ProductQuantityOutput productQuantityOutput = default!;

        public void OnProductQuantityUpdatedEventHandler(ProductQuantityEventArgs args)
        {
            productQuantityOutput.UpdateQuantity(args);
        }

    }



}

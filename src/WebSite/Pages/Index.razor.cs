using Microsoft.AspNetCore.Components;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Pages
{
    public partial class Index
    {
        private IEnumerable<Product> displayedProducts = new List<Product>();
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages;
        private string searchText = "";

        [Inject]
        private ProductsService ProductsService { get; set; }


        [Parameter]
        public bool ProductsLoading { get; set; }

        [Parameter]
        public string ErrorMessage { get; set; }

        [Parameter]
        public long LastProductId { get; set; }

        public Index()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            
            base.OnInitialized();
            await FetchProductsAsync();
        }


        private async Task FetchProductsAsync()
        {
            
            await ManagePageState(async ()=> await ProductsService.GetAllProducts(LastProductId, pageSize, CancellationToken.None));
          
        }

        private async Task Search()
        {
            
            await ManagePageState(async () => await ProductsService.GetAllProductsByKeyword(LastProductId, pageSize, searchText, CancellationToken.None));
        }

        private async Task ManagePageState(Func<Task<IEnumerable<Product>>> productServiceHandler)
        {
            try
            {
                this.ErrorMessage = string.Empty;
                ProductsLoading = true;
               

                var products = await productServiceHandler();
                if (products.Any())
                {
                    this.LastProductId = products.Max(product => product.Id);
                    this.displayedProducts = products;
                }
                else 
                {
                    this.ErrorMessage = "No products found";
                }
                
                
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "There was a problem fetching the products, If the problem persists please contact Support";

             
            }finally 
            { 
                ProductsLoading = false;
           

            }

        }
    }
}

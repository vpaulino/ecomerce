using Microsoft.AspNetCore.Components;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Products.Components
{
    public partial class ProductsTable
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
        public long TotalRecordsCount { get; private set; }
        [Parameter]
        public string ErrorMessage { get; set; }

        [Parameter]
        public long LastProductId { get; set; }

        public ProductsTable()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            
            base.OnInitialized();
            await FetchProductsAsync();
        }


        private async Task FetchProductsAsync()
        {
            
            await ManageTableState(async ()=> await ProductsService.GetAllProducts(LastProductId, pageSize, CancellationToken.None));
          
        }

        private async Task PreviousOnClick() 
        {
            if (!string.IsNullOrEmpty(this.searchText))
            {
                await ManageTableState(async () => await ProductsService.GetAllProductsByKeyword(LastProductId, pageSize, searchText, CancellationToken.None));
                return;
            }
            await ManageTableState(async () => await ProductsService.GetAllProducts(LastProductId - 2*pageSize, pageSize, CancellationToken.None));
            this.currentPage--;
        }

        private async Task NextOnClick()
        {
            this.currentPage++;
            if (!string.IsNullOrEmpty(this.searchText)) 
            {
                await ManageTableState(async () => await ProductsService.GetAllProductsByKeyword(LastProductId, pageSize, searchText, CancellationToken.None));
                return;
            }
            await ManageTableState(async () => await ProductsService.GetAllProducts(LastProductId, pageSize, CancellationToken.None));

        }

        private async Task Search()
        {
            
            await ManageTableState(async () => await ProductsService.GetAllProductsByKeyword(LastProductId, pageSize, searchText, CancellationToken.None));
        }
 

        private async Task ManageTableState(Func<Task<IEnumerable<Product>>> productServiceHandler)
        {
            try
            {
                this.ErrorMessage = string.Empty;
                ProductsLoading = true;

                this.TotalRecordsCount = await this.ProductsService.GetProductsCount(CancellationToken.None);
                 
                var products = await productServiceHandler();
                if (products.Any())
                {
                    this.LastProductId = products.Max(product => product.Id)+1;
                    this.displayedProducts = products;
                    if (displayedProducts.Count() < this.pageSize)
                    {
                        this.TotalRecordsCount = displayedProducts.Count();
                        this.totalPages = 1;
                    }
                    else 
                    {
                        this.totalPages = ((int)(TotalRecordsCount / this.pageSize)) + 1;
                    }
                    
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

        private bool IsPreviousDisabled() 
        {
            return this.currentPage == 1;
        }

        private bool IsNextDisabled()
        {
            return this.currentPage == this.totalPages;
        }
    }
}

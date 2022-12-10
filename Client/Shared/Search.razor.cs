using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Ecommerce.Client.Shared
{
    partial class Search
    {
        private string searchText = string.Empty;
        private List<string> suggestions = new List<string>();

        //To set the focus into the input field
        protected ElementReference searchInput;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await searchInput.FocusAsync();
            }
        }

        public void SearchProducts()
        {
            if (!string.IsNullOrEmpty(searchText))
                NavigationManager.NavigateTo($"search/*");

            NavigationManager.NavigateTo($"search/{searchText}/1");
        }

        public async Task HandleSearch(KeyboardEventArgs args)
        {
            if (args.Key == null || args.Key.Equals("Enter"))
            {
                SearchProducts();
            }
            else if (searchText.Length > 1)
            {
                suggestions = await ProductService.GetProductSearchSuggestions(searchText);
            }
        }
    }
}

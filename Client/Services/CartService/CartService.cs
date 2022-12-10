using Blazored.LocalStorage;
using Ecommerce.Shared;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public CartService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _httpClient = http;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            cart ??= new List<CartItem>();

            var existing = cart.Find(x => x.ProductId== cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);
            if(existing != null)
            {
                existing.Quantity += cartItem.Quantity;
            }
            else
            {
                cart.Add(cartItem);
            }

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            cart ??= new List<CartItem>();

            return cart;
        }

        public async Task<List<CartProductResponseDto>> GetCartProducts()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            var response = await _httpClient.PostAsJsonAsync("api/cart/products", cart);
            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponseDto>>>();
            return cartProducts.Data;
        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }
            var cartItem = cart.Find(x => x.ProductId == productId && x.ProductTypeId == productTypeId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
        }

        public async Task UpdateQuantity(CartProductResponseDto product)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }
            var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.ProductTypeId == product.ProductTypeId);

            if (cartItem != null)
            {
                cartItem.Quantity= product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }
}

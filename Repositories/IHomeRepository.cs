namespace AgriShoppingCartMvcUI
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetProducts(string sTerm = "", int CategoryId = 0);
        Task<IEnumerable<Category>> Categories();
        Task<Product?> GetProductById(int id); // Add this line
    }
}
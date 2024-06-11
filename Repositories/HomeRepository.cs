

using Microsoft.EntityFrameworkCore;

namespace AgriShoppingCartMvcUI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> Categories()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts(string sTerm = "", int CategoryId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> Products = await (from Product in _db.Products
                         join Category in _db.Categories
                         on Product.CategoryId equals Category.Id
                         where string.IsNullOrWhiteSpace(sTerm) || (Product != null && Product.ProductName.ToLower().StartsWith(sTerm))
                         select new Product
                         {
                             Id = Product.Id,
                             Image = Product.Image,
                             BrandName = Product.BrandName,
                             ProductName = Product.ProductName,
                             CategoryId = Product.CategoryId,
                             Price = Product.Price,
                             CategoryName = Category.CategoryName
                         }
                         ).ToListAsync();
            if (CategoryId > 0)
            {

                Products = Products.Where(a => a.CategoryId == CategoryId).ToList();
            }
            return Products;

        }
        public async Task<Product?> GetProductById(int id)
        {
            var product = await _db.Products
                .Include(p => p.Category) // Assuming you want to include Category details
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                // Optionally, if you want to set the CategoryName property which is not mapped
                product.CategoryName = product.Category?.CategoryName;
            }

            return product;
        }
    }
}

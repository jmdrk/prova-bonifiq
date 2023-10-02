using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Utilities;

namespace ProvaPub.Services
{
	public class ProductService
	{
		private readonly TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

        public Paginator<Product> ListProducts(int page, int pageSize)
        {
            var query = _ctx.Products.AsQueryable();

            return Paginator<Product>.CreateAsync(query, page, pageSize).Result;
        }


    }
}

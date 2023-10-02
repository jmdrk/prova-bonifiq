using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Utilities;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;
        public Parte2Controller(ProductService productService, CustomerService customerService)
		{
            _productService = productService;
			_customerService = customerService;
		}
	
		[HttpGet("products")]
		public ActionResult<Paginator<Product>> ListProducts(int? page, int? pageSize)
		{
            int pageIndex = page ?? 1;
            int actualPageSize = pageSize ?? PaginationDefaults.PageSize;

            var listaPaginada = _productService.ListProducts(pageIndex, actualPageSize);
			return Ok(listaPaginada);
		}

		[HttpGet("customers")]
		public ActionResult<Paginator<Customer>> ListCustomers(int? page, int? pageSize)
		{
            int pageIndex = page ?? 1;
            int actualPageSize = pageSize ?? PaginationDefaults.PageSize;

            var listaPaginada = _customerService.ListCustomers(pageIndex, actualPageSize);
			return Ok(listaPaginada);
		}
	}
}

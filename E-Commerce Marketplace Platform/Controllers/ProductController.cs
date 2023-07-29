using E_CommerceMarketplace.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Marketplace_Platform.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IRepository repository;
		public ProductController(IRepository _repository)
		{
			repository= _repository;
		}

		//[HttpGet]
		//public async Task<IActionResult> Add()
		//{

		//}

		//[HttpPost]
		//public async Task<IActionResult> Add()
		//{

		//}
	}
}

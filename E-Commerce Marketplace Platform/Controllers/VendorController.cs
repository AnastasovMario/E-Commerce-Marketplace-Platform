using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Constants;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Vendor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Marketplace_Platform.Controllers
{
	[Authorize]
	public class VendorController : Controller
	{
		private readonly IVendorService vendorService;

		public VendorController(IVendorService _vendorService)
		{
			vendorService = _vendorService;
		}

		[HttpGet]
		public async Task<IActionResult> Become()
		{
			if (await vendorService.ExistsById(User.Id()))
			{
                TempData[MessageConstants.ErrorMessage] = "Вие вече сте Продавач";

                return RedirectToAction("Index", "Home");
                //throw new ArgumentException("Tried to become a vendor without permission");				
			}

			var model = new BecomeVendorModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeVendorModel model)
		{
			var userId = User.Id();

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (await vendorService.ExistsById(userId))
			{
				TempData[MessageConstants.ErrorMessage] = "Вие вече сте Продавач";

				return RedirectToAction("Index", "Home");
			}

			if (await vendorService.UserWithPhoneNumberExists(model.PhoneNumber))
			{
				TempData[MessageConstants.ErrorMessage] = "Телефона вече съществува";

				return RedirectToAction("Index", "Home");
			}

			await vendorService.Create(userId, model);

			return RedirectToAction("All", "Product");
		}
	}
}

﻿using E_Commerce_Marketplace_Platform.Extensions;
using E_Commerce_Marketplace_Platform.Helpers;
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
		private readonly SanitizerHelper sanitizer;

        public VendorController(IVendorService _vendorService,
            SanitizerHelper _sanitizer)
		{
			vendorService = _vendorService;
			sanitizer = _sanitizer;
		}

		[HttpGet]
		public async Task<IActionResult> Become()
		{
			if (await vendorService.ExistsById(User.Id()))
			{
                TempData[MessageConstants.ErrorMessage] = "Вие вече сте Продавач";

                return RedirectToAction(nameof(HomeController.Index), "Home");
                //throw new ArgumentException("Tried to become a vendor without permission");				
			}

			var model = new BecomeVendorModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeVendorModel model)
		{
			var userId = User.Id();
            model.FirstName = sanitizer.Sanitize(model.FirstName);
            model.LastName = sanitizer.Sanitize(model.LastName);
            model.PhoneNumber = sanitizer.Sanitize(model.PhoneNumber);

            if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (await vendorService.ExistsById(userId))
			{
				TempData[MessageConstants.ErrorMessage] = "Вие вече сте Продавач";

				return RedirectToAction(nameof(HomeController.Index), "Home");
			}

			if (await vendorService.UserWithPhoneNumberExists(model.PhoneNumber))
			{
				TempData[MessageConstants.ErrorMessage] = "Телефона вече съществува";

				return RedirectToAction(nameof(HomeController.Index), "Home");
			}

            await vendorService.Create(userId, model);

			return RedirectToAction(nameof(ProductController.All), "Product");
		}
	}
}

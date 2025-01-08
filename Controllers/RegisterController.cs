
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AdityaMinerals.Controllers
{
	public class RegisterController : Controller
	{
		// GET: Register
		public dynamic Register()
		{
			return View(); 
		}

		[HttpPost]
		public dynamic RegisterUser(string Username, string Password, string Email)
		{
			// Add your registration logic here
			ViewBag.Message = "Registration successful!";
			return RedirectToAction("Index", "Login");
		}
	}
}
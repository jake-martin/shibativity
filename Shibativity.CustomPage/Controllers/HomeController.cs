using System;
using System.Web.Mvc;

namespace Shibativity.CustomPage.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			try
			{
				var currentUserEmail = Relativity.CustomPages.ConnectionHelper.Helper().GetAuthenticationManager().UserInfo.EmailAddress;
				var imageUrl = ImageEndpoint.GetShibePicture();

				ViewBag.Email = currentUserEmail;
				ViewBag.Image = imageUrl;
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.Message);
			}

			return View();
		}

	}
}
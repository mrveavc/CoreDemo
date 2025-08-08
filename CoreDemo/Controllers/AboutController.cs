using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{

	public class AboutController : Controller
	{
		AboutManager abm=new AboutManager(new EfAboutRepository());
		[AllowAnonymous]
		public IActionResult Index()
		{
			var values = abm.GetList();

			return View(values);
		}
		public PartialViewResult SocialMediaAbout()
		{
			return PartialView();
		}
	}
}

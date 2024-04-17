using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.About
{
	public class AboutList :ViewComponent
	{
		AboutManager bm = new AboutManager(new EfAboutRepository());
		public IViewComponentResult Invoke()
		{

			var values = bm.GetList();
			return View(values);
		}
	}
}

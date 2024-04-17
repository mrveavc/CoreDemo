using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	[Authorize] //Tümüne etki eder.
	public class WriterController : Controller
	{
		//	[Authorize]
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult WriterProfile(){
			return View();
		}
	//	[Authorize]
		public IActionResult WriterMail()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult Test()
		{
			return View();
		}
        [AllowAnonymous]

        public PartialViewResult WriterNavbarPartial()
		{
			return PartialView();
		}
        [AllowAnonymous]

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
       
    }
}

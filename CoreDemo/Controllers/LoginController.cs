using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class LoginController : Controller
	{
		[AllowAnonymous] //Proje içerisindeki koyulan tüm kurallardan muaf ol
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Index(Writer p)
		{
			Context c=new Context();
			var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
			if(datavalue != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,p.WriterMail)
				};
				var useridentity=new ClaimsIdentity(claims,"a");
				ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
				await HttpContext.SignInAsync(principal); //şifreli formatta cookie oluşturmak için
				return RedirectToAction("Index", "Dashboard");
			}
            else
            {
				return View();
                
            }
            //Context c=new Context();
            //var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            //if (datavalue != null)
            //{
            //	HttpContext.Session.SetString("username", p.WriterMail);
            //	return RedirectToAction("Index", "Writer");
            //}
            //else
            //{
            //	return View();
            //}

        }

	}
}

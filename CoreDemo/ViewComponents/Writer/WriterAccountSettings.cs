using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Concrete;
using System.Linq;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterAccountSettings : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;

            Context c = new Context();
            var ImageUrl = c.Users.Where(x => x.UserName == username).Select(y => y.ImageUrl).FirstOrDefault();

            ViewBag.i = ImageUrl;
            ViewBag.v = username;

            return View();
        }
    }
}


using System.Linq;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterNavbar : ViewComponent
    {
        Context c=new Context();
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var imageUrl= c.Users.Where(x=>x.UserName==username).Select(x=>x.ImageUrl).FirstOrDefault();
            ViewBag.v = username;
            ViewBag.i=imageUrl;
            return View();
        }
    }
}
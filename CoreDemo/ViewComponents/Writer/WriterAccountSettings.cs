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
            ViewBag.v = username;

            return View();
        }
    }
}


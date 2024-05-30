using System.Collections.Generic;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());

        public IActionResult InBox()
        {
            int id = 1;
            var values = mm.GetInboxListByWriter(id);
            return View(values);
            
        }
        //[HttpGet]
        public IActionResult MessageDetails(int id)
        {
            var value = mm.TGetById(id);
        
            return View(value);
        }


       


    }
}

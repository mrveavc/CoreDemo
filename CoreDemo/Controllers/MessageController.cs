using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Controllers
{
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        private readonly UserManager<AppUser>  _userManager;

        Context c = new Context();

        public MessageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult InBox()
        {
           
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerID);
            return View(values);
            
        }
        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetSendboxListByWriter(writerID);
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = mm.TGetById(id);
        
            return View(value);
        }
        [HttpGet]
        public async Task<IActionResult> SendMessage() {

            List<SelectListItem> recieverUsers = (from x in await c.GetWritersAsync()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.WriterMail.ToString(),
                                                      Value = x.WriterID.ToString()
                                                  }).ToList();
            ViewBag.RecieverUser = recieverUsers;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message2 p)
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            p.SenderID=writerID;
            p.MessageStatus = true;
            p.MessageDate=Convert.ToDateTime(DateTime.Now.ToShortDateString());
            mm.TAdd(p);
            return RedirectToAction("Inbox");
        }





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context c = new Context();


        [AllowAnonymous]
        public IActionResult Index()
        {
            //var values=bm.GetList();
            var values = bm.GetBlogListWithCategory();
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
			var writerID = c.Blogs.Where(x => x.BlogID == id).Select(y => y.WriterID).FirstOrDefault(); 
            ViewBag.a= writerID;
			var values = bm.GetBlogById(id);

            return View(values);

        }
        public IActionResult BlogListByWriter()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID =c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault(); //Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
            var values = bm.GetListWithCategoryByWriterBm(writerID);
            return View(values);

        }
        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value=x.CategoryID.ToString(),
                                                   }).ToList();
            ViewBag.cv= categoryvalues;
            return View();

        }
        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            BlogValidator bv = new BlogValidator();
                ValidationResult result = bv.Validate(p);
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;
            if (result.IsValid)
                {
                    p.BlogStatus = true;
                    p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    p.WriterID = writerID;
                    bm.TAdd(p);

                    return RedirectToAction("BlogListByWriter", "Blog");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                    }
                }
                return View();

            }
         public IActionResult DeleteBlog(int id)
        {
            var blogvalue=bm.TGetById(id);
            bm.TDelete(blogvalue);  
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString(),
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
            var blogvalue = bm.TGetById(p.BlogID);
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            //p.WriterID = writerID;
            p.WriterID = blogvalue.WriterID; //mevcut yazar id'si
            //p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToString());
            p.BlogCreateDate = DateTime.Parse(blogvalue.BlogCreateDate.ToShortDateString()); //güncelleme sonrası tarih değişmiyor.
            //p.BlogStatus = true;
            p.BlogStatus = blogvalue.BlogStatus; //mevcut status
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }



    }
}
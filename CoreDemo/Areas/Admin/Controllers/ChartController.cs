using System.Collections.Generic;
using System.Linq;
using CoreDemo.Areas.Admin.Models;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryChart()
        {
            Context c=new Context();
            List<CategoryClass> list = new List<CategoryClass>();
            foreach(var category in c.Categories.ToList())  //kategorilerin isimleri ve bu kategori altında kaç blog yazıldığı
            {
                int blogCount = 0;

                foreach (var blog in c.Blogs.Where(x => x.CategoryID == category.CategoryID))
                {
                    blogCount++;
                }
                list.Add(new CategoryClass
                {
                    categoryname = category.CategoryName +" - " + blogCount + " blog" ,
                    
                    categorycount = blogCount,
                    
                });
                

            }
            //list.Add(new CategoryClass
            //{
            //    categoryname = "Teknoloji",
            //    categorycount = 10
            //});
            //list.Add(new CategoryClass
            //{
            //    categoryname = "Yazılım",
            //    categorycount = 14
            //});
            //list.Add(new CategoryClass
            //{
            //    categoryname = "Spor",
            //    categorycount = 5
            //});
            //list.Add(new CategoryClass
            //{
            //    categoryname = "Sinema",
            //    categorycount = 2
            //});

            return Json(new { jsonlist = list });
        }
    }
}
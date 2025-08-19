using BlogApiDemo.DataAccessLayer;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace BlogApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        [HttpGet]
        public IActionResult CategoryList()
        {
            Context c = new Context();
            var values = c.Categories.ToList();
            return Ok(values);

        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            Context c = new Context();
            cm.TAdd(category);
            c.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Context c = new Context();
            var category = c.Categories.Find(id);
           
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                cm.TDelete(category);
                c.SaveChanges();
                
                return Ok();
            }

        }
        [HttpPut]
        public IActionResult UpdateCategory(Category category) { 
            Context c = new Context();
            var cat = c.Categories.Find(category.CategoryID);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                cat.CategoryName = category.CategoryName;
                cat.CategoryDescription = category.CategoryDescription;
                cm.TUpdate(cat);
                c.SaveChanges();
                return Ok();
            }


        }

    }
}

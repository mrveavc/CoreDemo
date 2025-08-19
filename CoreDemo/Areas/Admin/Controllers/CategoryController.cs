using System;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager cm =new CategoryManager(new EfCategoryRepository());
        public IActionResult Index(int page=1)
        {
            var values=cm.GetList().ToPagedList(page,2); // Her sayfada 2 adet veri listelensin
            return View(values);
        }
        [HttpGet]
        public IActionResult CategoryAdd() { 
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category p)
        {
            CategoryValidator cv = new CategoryValidator();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.CategoryStatus = true;
                cm.TAdd(p);

                return RedirectToAction("Index", "Category");

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
        public IActionResult CategoryDelete(int id)
        {
            var value = cm.TGetById(id);
            cm.TDelete(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CategoryEdit(int id) { 
                
            var categoryvalue=cm.TGetById(id);
           
            return View(categoryvalue);
        
        }
        [HttpPost]
        public IActionResult CategoryEdit(Category p)
        {
            var categoryvalue = cm.TGetById(p.CategoryID);
            categoryvalue.CategoryID = p.CategoryID;
            categoryvalue.CategoryName=p.CategoryName;
            categoryvalue.CategoryDescription=p.CategoryDescription;
            categoryvalue.CategoryStatus=p.CategoryStatus;
            cm.TUpdate(categoryvalue);
            return RedirectToAction("Index");

        }
       
        public IActionResult ChangeStatus(int id, bool status)
        {
            var category = cm.TGetById(id); 
            if (category != null)
            {
                category.CategoryStatus = status; 
                cm.TUpdate(category);             
            }

            return RedirectToAction("Index"); 
        }
    }
}

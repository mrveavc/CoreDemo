using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        //EfCategoryRepository efCategoryRepository;
        ICategoryDal _categoryDal;

        //public CategoryManager()
        //{
        //    efCategoryRepository = new EfCategoryRepository();
        //}
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        //CategoryRepository categoryRepository = new CategoryRepository();
        //GenericRepository<Category> repo=new GenericRepository<Category>;

        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);

            //repo.Insert(category);

            //if (category.CategoryName!="" && category.CategoryDescription!="" && category.CategoryName.Length>=5 && category.CategoryStatus==true)
            //{
            //    categoryRepository.AddCategory(category);
            //}
            //else
            //{
            //    //Hata mesajı
            //}
          
        }

        public void CategoryDelete(Category category)
        {
            _categoryDal.Delete(category);
            //if (category.CategoryID!=0)
            //{
            //    repo.Delete(category);
                
            //}
        }

        public void CategoryUpdate(Category category)
        {
            _categoryDal.Update(category);
        }

        public Category GetById(int id)
        {
           return _categoryDal.GetById(id);
        }

        public List<Category> GetList()
        {
           return _categoryDal.GetListAll();
        }
    }
}

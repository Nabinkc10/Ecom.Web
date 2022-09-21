using ECom.Model.Models;
using ECom.Model.ViewModel;
using ECom.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Services
{
    public interface ICategoryService
    {
        (bool, string) Create(CategoryCreateViewModel model);
        (bool, string) Delete(int id);
        (bool, string) Edit(CategoryListViewModel model);
        List<CategoryListViewModel> GetAll();
        CategoryListViewModel GetById(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public List<CategoryListViewModel>GetAll()
        {
          var data =categoryRepository.GetAll();
            var ret = data.Select(p => new CategoryListViewModel()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Name = p.Name,
                ParentCategoryName = p.ParentCategory == null ? "" : p.ParentCategory.Name
            });
            return ret.ToList();
        }
        public (bool,string) Create(CategoryCreateViewModel model)
        {
            try
            {
                var cat = new Category()
                {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description= model.Description,
                };
                return categoryRepository.Create(cat);
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }

        }
        public CategoryListViewModel GetById(int id)
        {
            var data = categoryRepository.GetById(id);
            if(data!=null)
                {
                return new CategoryListViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    CategoryId = data.CategoryId,
                    ParentCategoryName = data.ParentCategory == null ? "" : data.ParentCategory.Name
                };
            }
            else
            {
                return null;
            }
        }
        public (bool, string) Edit(CategoryListViewModel model)
        {
            try
            {
                var existing = categoryRepository.GetById(model.Id);
                if (existing == null) return (false, "Record Not Found!");

                existing.Name=model.Name;
                existing.Description=model.Description;
                existing.CategoryId=model.CategoryId;

                return categoryRepository.Edit(existing);
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }

        }
        public (bool, string) Delete(int id)
        {
            try
            {
                var existing = categoryRepository.GetById(id);
                if (existing == null) return (false, "Record Not Found!");

                return categoryRepository.Delete(existing);
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }

        }
    }
}

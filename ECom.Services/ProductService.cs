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
    public interface IProductService
    {
        (bool, string) Create(ProductListViewModel model);
        (bool, string) Delete(int id);
        (bool, string) Edit(ProductListViewModel model);
        List<ProductListViewModel> GetAll();
        ProductListViewModel GetById(int id);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
            
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }
        public List<ProductListViewModel>GetAll()
        {
          var data =productRepository.GetAll();
            var ret = data.Select(p => new ProductListViewModel()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Name = p.Name,
                CategoryName = p.Category == null ? "" : p.Category.Name,
                PicturePath = p.PicturePath,
                price = p.price,
                Stock = p.Stock,
                Units = p.Units
            });
            return ret.ToList();
        }
        public (bool,string) Create(ProductListViewModel model)
        {
            try
            {
                var cat = new Product()
                {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description= model.Description,
                Units = model.Units,
                PicturePath = model.PicturePath,
                Stock = model.Stock,
                price = model.price,
                };
                return productRepository.Create(cat);
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }

        }
        public ProductListViewModel GetById(int id)
        {
            var data = productRepository.GetById(id);
            if(data!=null)
                {
                return new ProductListViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    CategoryId = data.CategoryId,
                    CategoryName = data.Category == null ? "" : data.Category.Name,
                    PicturePath =data.PicturePath,
                    price = data.price,
                    Stock=data.Stock,
                    Units=data.Units,

                };
            }
            else
            {
                return null;
            }
        }
        public (bool, string) Edit(ProductListViewModel model)
        {
            try
            {
                var existing = productRepository.GetById(model.Id);
                if (existing == null) return (false, "Record Not Found!");

                existing.Name=model.Name;
                existing.PicturePath = model.PicturePath;
                existing.Description=model.Description;
                existing.CategoryId=model.CategoryId;
                existing.Units = model.Units;
                existing.Stock = model.Stock;
                existing.price = model.price;
                
                

                return productRepository.Edit(existing);
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
                var existing = productRepository.GetById(id);
                if (existing == null) return (false, "Record Not Found!");

                return productRepository.Delete(existing);
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }

        }

      
    }
}

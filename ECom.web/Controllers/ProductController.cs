using ECom.Model.ViewModel;
using ECom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ECom.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IHostingEnvironment hostingEnvironment;
        public ProductController(IProductService productService, ICategoryService categoryService,IHostingEnvironment hostingEnvironment)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var data = productService.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductListViewModel model, IFormFile productpic)
        {
            ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
            if(ModelState.IsValid)
            {
                var filename = Guid.NewGuid().ToString();

                if (productpic.Length>0)
                {
                    var extension = Path.GetExtension(productpic.FileName);
                    var path = Path.GetFullPath(hostingEnvironment.WebRootPath);
                    var file = Path.Combine(path,"uploads","products", filename + extension);
                    using (var stream=System.IO.File.Create(file))
                    {
                        productpic.CopyTo(stream);
                    }
                    model.PicturePath = "/uploads/products/" + filename + extension;

                    
                    var res = productService.Create(model);
                    if(res.Item1)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var existing = productService.GetById(id);
            if (existing == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(existing);
        }
        [HttpPost]
        public IActionResult Edit(ProductListViewModel model, IFormFile productpic)
        {
            ViewBag.categorylist = new SelectList(categoryService.GetAll(), "Id", "Name");
            if (ModelState.IsValid)
            {
                ViewBag.productlist = new SelectList(productService.GetAll(), "Id", "Name");
                   var filename = Guid.NewGuid().ToString();

                if (productpic.Length > 0)
                {
                    var extension = Path.GetExtension(productpic.FileName);
                    var path = Path.GetFullPath(hostingEnvironment.WebRootPath);
                    var file = Path.Combine(path, filename + extension);
                    using (var stream = System.IO.File.Create(file))
                    {
                        productpic.CopyTo(stream);
                    }
                    model.PicturePath = "~/uploads/products/" + filename + extension;


                    var result = productService.Edit(model);
                    if (result.Item1)
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", result.Item2);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var existing = productService.GetById(id);
            if (existing == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.productList = new SelectList(productService.GetAll(), "Id", "Name");
            return View(existing);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            
            var res = productService.Delete(id);
            return RedirectToAction("Index");


        }
    }
}

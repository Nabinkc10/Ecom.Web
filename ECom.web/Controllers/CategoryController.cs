using ECom.Model.ViewModel;
using ECom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECom.web.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ICategoryService categoryService;
        //dependency injection
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var data = categoryService.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View();
        }



        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
                var res = categoryService.Create(model);
                if (res.Item1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", res.Item2);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var existing = categoryService.GetById(id);
            if (existing == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(existing);
        }
        [HttpPost]
        public IActionResult Edit(CategoryListViewModel model)
        {
            ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
            if (ModelState.IsValid)
            {
                ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
                var res = categoryService.Edit(model);
                if (res.Item1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", res.Item2);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var existing = categoryService.GetById(id);
            if (existing == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.categoryList = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(existing);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var res = categoryService.Delete(id);
            return RedirectToAction("Index");


        }
    }
}

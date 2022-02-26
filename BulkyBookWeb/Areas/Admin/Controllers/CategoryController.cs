using BulkyBook.DataAccess.DatabaseModel;
using BulkyBook.DataAccess.Interfaces;
using BulkyBook.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var objCategory = await _categoryRepository.GetAll();

            return View(objCategory);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryDto category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cannot exactly match the Name. ");
            }
            if (ModelState.IsValid)
            {
                await _categoryRepository.CreateCategory(category);
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = await _categoryRepository.GetById(Id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cannot exactly match the Name. ");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = await _categoryRepository.GetById(Id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            await _categoryRepository.Delete(Id);
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
using BulkyBook.Models.DatabaseModel;
using BulkyBook.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models.Products;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICoverTypeRepository _coverTypeRepository;

        public ProductController(ICategoryRepository categoryRepository, ICoverTypeRepository coverTypeRepository)
        {
            _categoryRepository = categoryRepository;
            _coverTypeRepository = coverTypeRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var objCoverType = _coverTypeRepository.GetAll();

            return View(objCoverType);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _categoryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _coverTypeRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CoverTypeId.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                return View(productViewModel);
            }
            else
            {
                //update product
            }

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //_coverTypeRepository.Update(model);
                TempData["success"] = "Cover Type updated successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? coverTypeId)
        {
            if (coverTypeId == null || coverTypeId == 0)
            {
                return NotFound();
            }
            var coverType = await _coverTypeRepository.GetById(coverTypeId);

            return View(coverType);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoverType(int coverTypeId)
        {
            await _coverTypeRepository.Delete(coverTypeId);
            TempData["success"] = "Cover Type deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
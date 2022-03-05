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
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(
            ICategoryRepository categoryRepository, 
            ICoverTypeRepository coverTypeRepository, 
            IWebHostEnvironment webHostEnvironment, 
            IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _coverTypeRepository = coverTypeRepository;
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var objProduct = await _productRepository.GetAll();

            return View(objProduct);
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
        public async Task<IActionResult> Upsert(ProductViewModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                await _productRepository.CreateProduct(model.Product);
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return NotFound();
            }

            var product = await _productRepository.GetById(productId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetById(productId);
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            await _productRepository.Delete(productId);
            TempData["success"] = "Product deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
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
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? productId)
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

            if (productId == null || productId == 0)
            {
                //create product
                return View(productViewModel);
            }
            else
            {
                //Update product
                productViewModel.Product = await _productRepository.GetById(productId);
                return View(productViewModel);
            }

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

                    if(model.Product.ImageUrl != null)
                    {
                        var oldImagesPath = Path.Combine(wwwRootPath, model.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagesPath))
                        {
                            System.IO.File.Delete(oldImagesPath);
                        }
                    }


                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if(model.Product.ProductId == 0)
                {
                    await _productRepository.CreateProduct(model.Product);
                }
                else
                {
                    _productRepository.Update(model.Product);
                }
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete(int? productId)
        //{
        //    if (productId == null || productId == 0)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _productRepository.GetById(productId);

        //    return View(product);
        //}


        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();

            return Json(new { data = products });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _productRepository.GetById(productId);
            if(product == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            await _productRepository.Delete(productId);
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}
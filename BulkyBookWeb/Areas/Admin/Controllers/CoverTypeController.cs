using BulkyBook.DataAccess.DatabaseModel;
using BulkyBook.DataAccess.Interfaces;
using BulkyBook.Models.CoverTypes;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly ICoverTypeRepository _coverTypeRepository;

        public CoverTypeController(ICoverTypeRepository coverTypeRepository)
        {
            _coverTypeRepository = coverTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var objCoverType = await _coverTypeRepository.GetAll();

            return View(objCoverType);
        }

        [HttpGet]
        public IActionResult CreateCoverType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCoverType(CoverTypeDto coverType)
        {
            if (ModelState.IsValid)
            {
                await _coverTypeRepository.CreateCoverType(coverType);
                TempData["success"] = "Cover Type created successfully!";
                return RedirectToAction("Index");
            }

            return View(coverType);
        }

        [HttpGet]
        public async Task<IActionResult> EditCoverType(int? coverTypeId)
        {
            if(coverTypeId == null || coverTypeId == 0)
            {
                return NotFound();
            }
            var coverType = await _coverTypeRepository.GetById(coverTypeId);

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCoverType(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _coverTypeRepository.Update(coverType);
                TempData["success"] = "Cover Type updated successfully!";
                return RedirectToAction("Index");
            }

            return View(coverType);
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
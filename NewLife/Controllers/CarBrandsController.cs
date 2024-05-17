using NewLife.Models;
using NewLife.Utility;
using Microsoft.AspNetCore.Mvc;

namespace NewLife.Controllers
{
    public class CarBrandsController : Controller
    {
        private readonly ICarBrandsRepository _carBrandRepository;

        public CarBrandsController(ICarBrandsRepository context)
        {
            _carBrandRepository = context;
        }

        public IActionResult Index()
        {
            List<CarBrands> objCarBrandsList = _carBrandRepository.GetAll().ToList();
            return View(objCarBrandsList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CarBrands carBrand)
        {
            if (ModelState.IsValid)
            {
                _carBrandRepository.Add(carBrand);
                _carBrandRepository.Save();
                TempData["Succeed"] = "Brand added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CarBrands carbrandDb = _carBrandRepository.Get(u => u.Id == id);
            if (carbrandDb == null)
            {
                return NotFound();
            }
            return View(carbrandDb);
        }

        [HttpPost]
        public IActionResult Update(CarBrands carBrand)
        {
            if (ModelState.IsValid)
            {
                _carBrandRepository.Update(carBrand);
                _carBrandRepository.Save();
                TempData["Succeed"] = "Brand update successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CarBrands carbrandDb = _carBrandRepository.Get(u => u.Id == id);
            if (carbrandDb == null)
            {
                return NotFound();
            }
            return View(carbrandDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            CarBrands? carBrand = _carBrandRepository.Get(u => u.Id == id);
            if (carBrand == null)
            {
                return NotFound();
            }
            _carBrandRepository.Delete(carBrand);
            _carBrandRepository.Save();
            TempData["Succeed"] = "Brand remove successfully";
            return RedirectToAction("Index");
        }
    }
}

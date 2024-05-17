using NewLife.Models;
using NewLife.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace McqueenRentCarDB.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandsRepository _carBrandRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public CarController(ICarRepository carRepository, ICarBrandsRepository carBrandRepository, IWebHostEnvironment webHostEnvironment)
        {
            _carRepository = carRepository;
            _carBrandRepository = carBrandRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //List<Car> objCarList = _carRepository.GetAll().ToList();
            List<Car> objCarList = _carRepository.GetAll(includeProps:"CarBrands").ToList();
            return View(objCarList);
        }

        public IActionResult AddUpdate(int? id)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            IEnumerable<SelectListItem> CarBrandsList = _carBrandRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Brand_Name,
                Value = k.Id.ToString()
            });
            ViewBag.CarBrandsList = CarBrandsList;

            if(id == null || id==0)
            {
                return View();

            }

            //Update
            else
            {
                Car? carDb = _carRepository.Get(u => u.Id == id);
                if (carDb == null)
                {
                    return NotFound();
                }
                return View(carDb);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Car car, IFormFile? file )
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string carPath = Path.Combine(wwwRootPath, @"img");

                if(file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(carPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    car.CarImgUrl = @"\img\" + file.FileName;

                }

                if (car.Id == 0)
                {
                    _carRepository.Add(car);
                    TempData["Succeed"] = "Car added successfully";
                }
                else
                {
                    _carRepository.Update(car);
                    TempData["Succeed"] = "Car update successfully";
                }
                _carRepository.Save();
                return RedirectToAction("Index", "Car");

            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Car carDb = _carRepository.Get(u => u.Id == id);
            if (carDb == null)
            {
                return NotFound();
            }
            return View(carDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Car? car = _carRepository.Get(u => u.Id == id);
            if(car == null)
            {
                return NotFound() ; 
            }
            _carRepository.Delete(car);
            _carRepository.Save();
            TempData["Succeed"] = "Car remove successfully";
            return RedirectToAction("Index");
        }
    }
}

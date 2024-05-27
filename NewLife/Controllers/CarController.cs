using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using NewLife.Models;

namespace NewLife.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public CarController(ICarRepository carRepository, IWebHostEnvironment webHostEnvironment)
        {
            _carRepository = carRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            List<Car> objCarList = _carRepository.GetAll().ToList();
            //List<Cars> objCarList = _carsRepository.GetAll(includeProps).ToList();
            return View(objCarList);
        }

        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> CarsList = _carRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Car_Name,
                Value = k.Id.ToString()
            }
           );
            ViewBag.CarsList = CarsList;

            if (id == null || id == 0) 
            {
                return View();
            }
            else
            {
                Car carDb = _carRepository.Get(u => u.Id == id); 
                if (carDb == null)
                {
                    return NotFound();
                }
                return View(carDb);
            }

        }
        [HttpPost]
        public IActionResult AddUpdate(Car car, IFormFile file)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string carPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
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
                    TempData["basarili"] = "The add process was successfully performed"; // uyarı mesajları için kullanılıyor.
                }
                else
                {
                    _carRepository.Update(car);
                    TempData["basarili"] = " The update process was successfully performed";

                }

                _carRepository.Save();   // kayıt etmezsen veritabanına işlenmez.
                return RedirectToAction("Index", "Car");
            }
            return View();
        }
        public IActionResult Delete(int? id) //soru işaretini temkinli olmak için koy çünkü int olup olmadığını kontrol etmek zorunda
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Car? carDb = _carRepository.Get(u => u.Id == id);
            if (carDb == null)
            {
                return NotFound();
            }
            return View(carDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Car? car = _carRepository.Get(u => u.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            _carRepository.Delete(car);
            _carRepository.Save();
            TempData["basarili"] = "The delete process was successfully performed";
            return RedirectToAction("Index", "Car");

        }
        public IActionResult CarList()
        {
            var car = _carRepository.GetAll();
            return View(car);
        }
        public IActionResult Details(int id)
        {
            var car = _carRepository.Get(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
    }
}

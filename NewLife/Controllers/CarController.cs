using NewLife.Models;
using NewLife.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace NewLife.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public CarController(ICarRepository carRepository,IWebHostEnvironment webHostEnvironment)
        {
            _carRepository = carRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Car> objCarList = _carRepository.GetAll().ToList();
            return View(objCarList);
        }

        public IActionResult AddUpdate(int? id)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
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

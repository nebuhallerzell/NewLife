using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewLife.Models;
using System.Linq;
using System.Threading.Tasks;
using NewLife.Utility;
using System.Diagnostics;

namespace NewLife.Controllers
{
    public class RentController : Controller
    {
        private readonly IRentRepository _rentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;

        public RentController(IRentRepository rentRepository, IUserRepository userRepository, ICarRepository carRepository)
        {
            _rentRepository = rentRepository;
            _userRepository = userRepository;
            _carRepository = carRepository;
        }

        public IActionResult Index()
        {
            var rents = _rentRepository.GetAll(includeProps: "Car").ToList();
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            IEnumerable<SelectListItem> CarList = _carRepository.GetAll()
                .Select(b => new SelectListItem
                {
                    Text = b.Car_Name,
                    Value = b.Id.ToString()
                }
                ); //araba marka adını çekebilmek için kullandım
            ViewBag.CarList = CarList;


            //List<Car> objCarList = _carRepository.GetAll().ToList();
            List<Rent> objRentList = _rentRepository.GetAll(includeProps: "Car").ToList();
            return View(objRentList);
        }

        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> CarList = _carRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Car_Name,
                Value = k.Id.ToString()
            }
           );
            ViewBag.CarList = CarList;

            if (id == null || id == 0) //ekleme
            {
                return View();
            }
            else //guncelleme
            {
                Rent? rentDb = _rentRepository.Get(u => u.id == id); 
                {
                    return NotFound();
                }
                return View(rentDb);
            }

        }
        [HttpPost]
        public IActionResult AddUpdate(Rent rent)
        {
            if (ModelState.IsValid)
            {
                if (rent.Id == 0)
                {
                    _rentRepository.Add(rent);
                    TempData["basarili"] = "The add process was successfully performed";
                }
                else
                {
                    _rentRepository.Update(rent);
                    TempData["basarili"] = " The update process was successfully performed";

                }

                _rentRepository.Save();  
                return RedirectToAction("Index", "Rent");
            }
            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewLife.Models;
using NewLife.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace NewLife.Controllers
{
    public class RentController : Controller
    {
        private readonly IRentRepository _rentRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;

        public RentController(IRentRepository rentRepository, ICarRepository carRepository, IUserRepository userRepository)
        {
            _rentRepository = rentRepository;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var rents = _rentRepository.GetAllRents();
            return View(rents);
        }
        private void SetViewBags()
        {
            ViewBag.CarsList = _carRepository.GetAll().Select(c => new SelectListItem
            {
                Text = c.Car_Name,
                Value = c.Id.ToString()
            }).ToList();

            ViewBag.UsersList = _userRepository.GetAll().Select(u => new SelectListItem
            {
                Text = $"{u.User_Name} {u.User_Surname}",
                Value = u.Id.ToString()
            }).ToList();
        }

        public IActionResult Rent(int? id)
        {
            SetViewBags();

            if (id==null || id == 0)
            {
                return View(new Rent());
            }
            else
            {
                Rent rentDb = _rentRepository.Get(r=>r.Id==id);
                if(rentDb == null)
                {
                    return NotFound();
                }
                return View(rentDb);
            }
        }

        [HttpPost]
        public IActionResult Rent(Rent rent)
        {
            if (ModelState.IsValid)
            {
                if(rent.Id == 0)
                {
                    _rentRepository.Add(rent);

                }
                else
                {
                    _rentRepository.Update(rent);

                }
                _rentRepository.Save();
                return RedirectToAction("Index");
            }
            SetViewBags();

            return View(rent);

        }
        public IActionResult UserRent(int? id, int? carId)
        {
            SetViewBags();

            if (id == null || id == 0)
            {
                Rent newRent = new Rent();

                if (carId != null)
                {
                    var car = _carRepository.Get(c => c.Id == carId);
                    if (car != null)
                    {
                        newRent.CarId = car.Id;
                        newRent.Car = car;
                    }
                }

                var user = _userRepository.Get(u => u.User_Name == User.Identity.Name);
                if (user != null)
                {
                    newRent.UserId = user.Id;
                    newRent.User = user;
                }

                return View(newRent);
            }
            else
            {
                Rent rentDb = _rentRepository.Get(r => r.Id == id);
                if (rentDb == null)
                {
                    return NotFound();
                }
                return View(rentDb);
            }
        }

        [HttpPost, ActionName("UserRent")]
        public IActionResult UserRentPost(Rent rent)
        {

            if (rent.Id == 0)
            {
                _rentRepository.Add(rent);
            }
            else
            {
                _rentRepository.Update(rent);
            }

            _rentRepository.Save();
            TempData["basarili"] = "Kayıt işlemi başarıyla gerçekleştirildi";
            return RedirectToAction("Index");
        }

    }
}

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

        [Authorize]
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

            if (id == null || id == 0)
            {
                return View(new Rent());
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

        [HttpPost]
        public IActionResult Rent(Rent rent)
        {
            if (ModelState.IsValid)
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

                return RedirectToAction("Index");
            }
            SetViewBags();
            return View(rent);
        }

        [Authorize]
        public IActionResult UserRent(int? id, int? carId)
        {
            Rent newRent = new Rent();
            if (id == null || id == 0)
            {
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
            }
            else
            {
                newRent = _rentRepository.Get(r => r.Id == id);
                if (newRent == null)
                {
                    return NotFound();
                }
            }
            ViewBag.CarName = newRent.Car?.Car_Name;
            ViewBag.UserName = $"{newRent.User?.User_Name} {newRent.User?.User_Surname}";
            return View(newRent);
        }


        [Authorize]
        [HttpPost, ActionName("UserRent")]
        public IActionResult UserRentPost(Rent rent)
        {
            if (ModelState.IsValid)
            {
                var existingCar = _carRepository.Get(c => c.Id == rent.CarId);
                double carPrice = existingCar != null ? existingCar.Car_Price : 0;
                rent.CalculateRentPrice(carPrice);

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
                return RedirectToAction("RentList");
            }
            return View(rent);
        }


        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Rent rentDb = _rentRepository.Get(u => u.Id == id);
            if (rentDb == null)
            {
                return NotFound();
            }
            return View(rentDb);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Rent? rent = _rentRepository.Get(u => u.Id == id);
            if (rent == null)
            {
                return NotFound();
            }
            _rentRepository.Delete(rent);
            _rentRepository.Save();
            TempData["Succeed"] = "User removed successfully";
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult RentList()
        {
            // Mevcut kullanıcının kimliğini alıyoruz.
            var currentUserName = User.Identity.Name;

            // Kullanıcı bilgilerini veritabanından alıyoruz.
            var currentUser = _userRepository.Get(u => u.User_Name == currentUserName);

            if (currentUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // GetAllRents() sonucunu IEnumerable<Rent> olarak alıyoruz.
            var rentsObject = _rentRepository.GetAllRents();
            if (rentsObject is IEnumerable<Rent> rents)
            {
                // Kullanıcının ID'siyle eşleşen rentleri çekiyoruz.
                var userRents = rents.Where(r => r.UserId == currentUser.Id).ToList();
                return View(userRents);
            }
            else
            {
                return NotFound("Kiralama bilgileri uygun formatta değil.");
            }
        }


    }
}

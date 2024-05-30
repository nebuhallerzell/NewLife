using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewLife.Models;
using NewLife.Utility;
using System.Security.Claims;
namespace NewLife.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UygulamaDbContext _uygulamaDbContext;

        public UserController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, UygulamaDbContext uygulamaDbContext)
        {
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _uygulamaDbContext = uygulamaDbContext;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User_Name,User_Surname,User_Email,User_Password,User_Phone,User_Adress")] User user)
        {
            if (ModelState.IsValid)
            {
                _uygulamaDbContext.Add(user);
                await _uygulamaDbContext.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }
            return View(user);
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Index()
        {
            var userMail = HttpContext.User.FindFirst("userMail").Value.ToString();
            List<User> objUserList = _userRepository.GetAll().ToList();
            return View(objUserList);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            User userDb = _userRepository.Get(u => u.Id == id);
            if (userDb == null)
            {
                return NotFound();
            }
            return View(userDb);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            User? user = _userRepository.Get(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.Delete(user);
            _userRepository.Save();
            TempData["Succeed"] = "User removed successfully";
            if (user.User_Type == "Admin")
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Profile", "User");
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userMail, string password)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.Get(u => u.User_Email == userMail);

                if (user == null)
                {
                    TempData["Error"] = "Kullanıcı Bulunamadı";
                }
                else if (user.User_Password != password)
                {
                    TempData["Error"] = "Şifre Yanlış";
                }
                else
                {
                    List<Claim> claims = new List<Claim>()
            {
                new Claim("userMail", user.User_Email),
                new Claim("userId", user.Id.ToString()),
                new Claim("type", user.User_Type),
                new Claim(ClaimTypes.Name, user.User_Name)
            };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(principal);

                    if (user.User_Type == "Admin")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AddUpdate(int? id)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (id == null || id == 0)
            {
                return View();
            }

            // Update
            else
            {
                User? userDb = _userRepository.Get(u => u.Id == id);
                if (userDb == null)
                {
                    return NotFound();
                }
                return View(userDb);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddUpdate(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    _userRepository.Add(user);
                    TempData["Succeed"] = "User added successfully";
                }
                else
                {
                    var existingUser = _userRepository.Get(u => u.Id == user.Id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    existingUser.User_Name = user.User_Name;
                    existingUser.User_Surname = user.User_Surname;
                    existingUser.User_Email = user.User_Email;
                    existingUser.User_Password = user.User_Password;
                    existingUser.User_Phone = user.User_Phone;

                    _userRepository.Update(existingUser);
                    TempData["Succeed"] = "User updated successfully";
                }

                _userRepository.Save();
                if (user.User_Type == "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Profile", "User");
                }
            }
            return View(user);
        }
        [Authorize]
        public IActionResult Profile()
        {
            var user = _userRepository.Get(u => u.User_Name == User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


    }
}

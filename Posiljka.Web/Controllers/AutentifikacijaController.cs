using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.EF;
using MyApp.Data.EntityModels;
using MyApp.Web.Helper.mvc;
using MyApp.Web.ViewModels;

namespace MyApp.Web.Controllers
{
    public class AutentifikacijaController : MyMvcBaseController
    {

        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                SavePassword = true,
            });
        }

        public IActionResult Login(LoginVM input)
        {
            KorisnickiNalog korisnik = _db.KorisnickiNalog
                .SingleOrDefault(x => x.KorisnickoIme == input.Username /*&& x.Lozinka == input.password*/);

            if (korisnik == null)
            {
                TempData["error_poruka"] = "pogrešan username ili password";
                return View("Index", input);
            }

            HttpContext.SetLogiraniKorisnik(korisnik, input.SavePassword);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {

            return RedirectToAction("Index");
        }

        public AutentifikacijaController(MyContext db) : base(db)
        {
        }
    }
}
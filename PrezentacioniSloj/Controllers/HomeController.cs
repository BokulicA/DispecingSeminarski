using Microsoft.AspNetCore.Mvc;

namespace PrezentacioniSloj.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Ako je korisnik već prijavljen, redirektuj na njegov dashboard
            var tipKorisnika = HttpContext.Session.GetString("TipKorisnika");

            if (!string.IsNullOrEmpty(tipKorisnika))
            {
                return tipKorisnika switch
                {
                    "Admin" => RedirectToAction("Index", "Admin"),
                    "Dispečer" => RedirectToAction("Index", "Dispecer"),
                    "Klijent" => RedirectToAction("Index", "Klijent"),
                    _ => View()
                };
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
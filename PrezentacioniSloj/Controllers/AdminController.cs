using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.Models;
using SlojPodataka.Klase;

namespace PrezentacioniSloj.Controllers
{
    public class AdminController : Controller
    {
        private readonly KorisnikServis _korisnikServis;
        private readonly VozacServis _vozacServis;
        private readonly KamionServis _kamionServis;
        private readonly TransportniNalogServis _nalogServis;

        public AdminController(
            KorisnikServis korisnikServis,
        VozacServis vozacServis,
            KamionServis kamionServis,
            TransportniNalogServis nalogServis)
        {
            _korisnikServis = korisnikServis;
            _vozacServis = vozacServis;
            _kamionServis = kamionServis;
            _nalogServis = nalogServis;
        }

        // GET: /Admin/Index
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            return View();
        }

        // ===== KORISNICI =====

        // GET: /Admin/Korisnici
        public IActionResult Korisnici()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            var ds = _korisnikServis.Prikazi();
            return View(ds);
        }

        // GET: /Admin/NoviKorisnik
        [HttpGet]
        public IActionResult NoviKorisnik()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            return View();
        }

        // POST: /Admin/NoviKorisnik
        [HttpPost]
        public IActionResult NoviKorisnik(NoviKorisnikModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var uspesno = _korisnikServis.Dodaj(new Korisnik
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                KorisnickoIme = model.KorisnickoIme,
                Lozinka = model.Lozinka,
                TipKorisnika = model.TipKorisnika
            });

            if (!uspesno)
            {
                ModelState.AddModelError(string.Empty, _korisnikServis.LastError);
                return View(model);
            }

            TempData["Success"] = "Korisnik je uspesno dodat.";
            return RedirectToAction("Korisnici");
        }

        // ===== VOZACI =====

        // GET: /Admin/Vozaci
        public IActionResult Vozaci()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            var ds = _vozacServis.Prikazi();
            return View(ds);
        }

        // GET: /Admin/NoviVozac
        [HttpGet]
        public IActionResult NoviVozac()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            return View();
        }

        // POST: /Admin/NoviVozac
        [HttpPost]
        public IActionResult NoviVozac(NoviVozacModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var uspesno = _vozacServis.Dodaj(new Vozac
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                BrojTelefona = model.BrojTelefona
            });

            if (!uspesno)
            {
                ModelState.AddModelError(string.Empty, _vozacServis.LastError);
                return View(model);
            }

            TempData["Success"] = "Vozac je uspesno dodat.";
            return RedirectToAction("Vozaci");
        }

        // ===== KAMIONI =====

        // GET: /Admin/Kamioni
        public IActionResult Kamioni()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            var ds = _kamionServis.Prikazi();
            return View(ds);
        }

        // GET: /Admin/NoviKamion
        [HttpGet]
        public IActionResult NoviKamion()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            return View();
        }

        // POST: /Admin/NoviKamion
        [HttpPost]
        public IActionResult NoviKamion(NoviKamionModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var uspesno = _kamionServis.Dodaj(new Kamion
            {
                Registracija = model.Registracija,
                Marka = model.Marka,
                Nosivost = model.Nosivost
            });

            if (!uspesno)
            {
                ModelState.AddModelError(string.Empty, _kamionServis.LastError);
                return View(model);
            }

            TempData["Success"] = "Kamion je uspesno dodat.";
            return RedirectToAction("Kamioni");
        }

        // GET: /Admin/SviNalozi
        public IActionResult SviNalozi()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Admin")
                return RedirectToAction("Prijava", "Nalog");

            var ds = _nalogServis.Prikazi();
            return View(ds);
        }
    }
}

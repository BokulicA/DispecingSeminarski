using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.Models;
using AplikacioniSloj;

namespace PrezentacioniSloj.Controllers
{
    public class NalogController : Controller
    {
        private readonly KorisnikServis _korisnikServis;

        public NalogController(KorisnikServis korisnikServis)
        {
            _korisnikServis = korisnikServis;
        }

        // GET: /Nalog/Prijava
        [HttpGet]
        public IActionResult Prijava()
        {
            return View();
        }

        // POST: /Nalog/Prijava
        [HttpPost]
        public IActionResult Prijava(PrijavaModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Poziv servisa za autentifikaciju
            var korisnik = _korisnikServis.Prijavi(model.KorisnickoIme, model.Lozinka);

            if (korisnik == null)
            {
                ModelState.AddModelError(string.Empty, _korisnikServis.LastError);
                return View(model);
            }

            // Postavljanje sesije
            HttpContext.Session.SetInt32("KorisnikID", korisnik.KorisnikID);
            HttpContext.Session.SetString("Ime", korisnik.Ime);
            HttpContext.Session.SetString("Prezime", korisnik.Prezime);
            HttpContext.Session.SetString("KorisnickoIme", korisnik.KorisnickoIme);
            HttpContext.Session.SetString("TipKorisnika", korisnik.TipKorisnika);

            // Redirekcija na osnovu tipa korisnika
            return korisnik.TipKorisnika switch
            {
                "Admin" => RedirectToAction("Index", "Admin"),
                "Dispecer" => RedirectToAction("Index", "Dispecer"),
                "Klijent" => RedirectToAction("Index", "Klijent"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // GET: /Nalog/Registracija
        [HttpGet]
        public IActionResult Registracija()
        {
            return View();
        }

        // POST: /Nalog/Registracija
        [HttpPost]
        public IActionResult Registracija(RegistracijaModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Poziv servisa za registraciju
            var uspesno = _korisnikServis.Registruj(
                model.Ime,
                model.Prezime,
                model.KorisnickoIme,
                model.Lozinka,
                model.PotvrdaLozinke
            );

            if (!uspesno)
            {
                ModelState.AddModelError(string.Empty, _korisnikServis.LastError);
                return View(model);
            }

            TempData["Success"] = "Nalog je kreiran. Prijavite se.";
            return RedirectToAction("Prijava");
        }

        // GET: /Nalog/Odjava
        [HttpGet]
        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Prijava");
        }
    }
}


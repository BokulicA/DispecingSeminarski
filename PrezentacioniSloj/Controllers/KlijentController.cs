using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.Models;

namespace PrezentacioniSloj.Controllers
{
    public class KlijentController : Controller
    {
        private readonly TransportniNalogServis _nalogServis;

        public KlijentController(TransportniNalogServis nalogServis)
        {
            _nalogServis = nalogServis;
        }

        // GET: /Klijent/Index
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Klijent")
                return RedirectToAction("Prijava", "Nalog");

            return View();
        }

        // GET: /Klijent/MojiNalozi
        public IActionResult MojiNalozi()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Klijent")
                return RedirectToAction("Prijava", "Nalog");

            var korisnikId = HttpContext.Session.GetInt32("KorisnikID");
            if (korisnikId == null)
                return RedirectToAction("Prijava", "Nalog");

            var ds = _nalogServis.PrikaziPoKlijentu(korisnikId.Value);
            return View(ds);
        }

        // GET: /Klijent/NoviNalog
        [HttpGet]
        public IActionResult NoviNalog()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Klijent")
                return RedirectToAction("Prijava", "Nalog");

            return View();
        }

        // POST: /Klijent/NoviNalog
        [HttpPost]
        public IActionResult NoviNalog(NoviNalogModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var korisnikId = HttpContext.Session.GetInt32("KorisnikID");
            if (korisnikId == null)
                return RedirectToAction("Prijava", "Nalog");

            var uspesno = _nalogServis.Dodaj(
                korisnikId.Value,
                model.Naziv,
                model.PolaznaDestinacija,
                model.KrajnjaDestinacija,
                model.Nosivost
            );

            if (!uspesno)
            {
                ModelState.AddModelError(string.Empty, _nalogServis.LastError);
                return View(model);
            }

            TempData["Success"] = "Nalog je uspesno kreiran.";
            return RedirectToAction("MojiNalozi");
        }
    }
}


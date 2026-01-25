using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.Models;

namespace PrezentacioniSloj.Controllers
{
    public class DispecerController : Controller
    {
        private readonly TransportniNalogServis _nalogServis;
        private readonly VozacServis _vozacServis;
        private readonly KamionServis _kamionServis;

        public DispecerController(
            TransportniNalogServis nalogServis,
            VozacServis vozacServis,
            KamionServis kamionServis)
        {
            _nalogServis = nalogServis;
            _vozacServis = vozacServis;
            _kamionServis = kamionServis;
        }

        // GET: /Dispecer/Index
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Dispečer")
                return RedirectToAction("Prijava", "Nalog");

            return View();
        }

        // GET: /Dispecer/KreiraniNalozi
        public IActionResult KreiraniNalozi()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Dispečer")
                return RedirectToAction("Prijava", "Nalog");

            var ds = _nalogServis.PrikaziKreirane();
            return View(ds);
        }

        // GET: /Dispecer/DodeliNalog/5
        [HttpGet]
        public IActionResult DodeliNalog(int id)
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Dispečer")
                return RedirectToAction("Prijava", "Nalog");

            ViewBag.NalogID = id;
            ViewBag.Vozaci = _vozacServis.Prikazi();
            ViewBag.Kamioni = _kamionServis.Prikazi();

            return View();
        }

        // POST: /Dispecer/DodeliNalog
        [HttpPost]
        public IActionResult DodeliNalog(DodeliNalogModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NalogID = model.NalogID;
                ViewBag.Vozaci = _vozacServis.Prikazi();
                ViewBag.Kamioni = _kamionServis.Prikazi();
                return View(model);
            }

            var korisnikId = HttpContext.Session.GetInt32("KorisnikID");
            if (korisnikId == null)
                return RedirectToAction("Prijava", "Nalog");

            var uspesno = _nalogServis.Dodeli(
                model.NalogID,
                korisnikId.Value,
                model.VozacID,
                model.Registracija
            );

            if (!uspesno)
            {
                ModelState.AddModelError(string.Empty, _nalogServis.LastError);
                ViewBag.NalogID = model.NalogID;
                ViewBag.Vozaci = _vozacServis.Prikazi();
                ViewBag.Kamioni = _kamionServis.Prikazi();
                return View(model);
            }

            TempData["Success"] = "Nalog je uspesno dodeljen.";
            return RedirectToAction("KreiraniNalozi");
        }

        // GET: /Dispecer/SviNalozi
        public IActionResult SviNalozi()
        {
            if (HttpContext.Session.GetString("TipKorisnika") != "Dispečer")
                return RedirectToAction("Prijava", "Nalog");

            var ds = _nalogServis.Prikazi();
            return View(ds);
        }
    }
}

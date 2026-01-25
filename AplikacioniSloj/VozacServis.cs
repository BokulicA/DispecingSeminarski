using DomenskiSloj;
using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikacioniSloj
{
    public class VozacServis
    {
        private IVozacRepo _repo;
        private PoslovnaPravila _poslovnaPravila;
        public string? LastError { get; private set; }

        public VozacServis(IVozacRepo repo, PoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
            LastError = null;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveVozace();
        }

        public DataSet PrikaziPoID(int vozacId)
        {
            return _repo.DajVozacaPoID(vozacId);
        }

        public bool Dodaj(Vozac noviVozac)
        {
            // Validacija broja telefona
            if (!_poslovnaPravila.ValidacijaBrojaTelefona(noviVozac.BrojTelefona))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            var ok = _repo.NoviVozac(noviVozac);
            if (!ok) LastError = "Vozac nije dodat.";
            return ok;
        }

        public bool Obrisi(int vozacId)
        {
            var ok = _repo.ObrisiVozaca(vozacId);
            if (!ok) LastError = "Vozac nije obrisan.";
            return ok;
        }

        public bool Izmeni(int vozacId, Vozac noviVozac)
        {
            // Validacija broja telefona
            if (!_poslovnaPravila.ValidacijaBrojaTelefona(noviVozac.BrojTelefona))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            var ok = _repo.IzmeniVozaca(vozacId, noviVozac);
            if (!ok) LastError = "Vozac nije izmenjen.";
            return ok;
        }
    }
}

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
    public class KamionServis
    {
        private IKamionRepo _repo;
        private PoslovnaPravila _poslovnaPravila;
        public string? LastError { get; private set; }

        public KamionServis(IKamionRepo repo, PoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
            LastError = null;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveKamione();
        }

        public DataSet PrikaziPoRegistraciji(string registracija)
        {
            return _repo.DajKamionPoRegistraciji(registracija);
        }

        public bool Dodaj(Kamion noviKamion)
        {
            // Validacija registracije
            if (!_poslovnaPravila.ValidacijaRegistracije(noviKamion.Registracija))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            var ok = _repo.NoviKamion(noviKamion);
            if (!ok) LastError = "Kamion nije dodat.";
            return ok;
        }

        public bool Obrisi(string registracija)
        {
            var ok = _repo.ObrisiKamion(registracija);
            if (!ok) LastError = "Kamion nije obrisan.";
            return ok;
        }

        public bool Izmeni(string registracija, Kamion noviKamion)
        {
            // Validacija registracije
            if (!_poslovnaPravila.ValidacijaRegistracije(noviKamion.Registracija))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            var ok = _repo.IzmeniKamion(registracija, noviKamion);
            if (!ok) LastError = "Kamion nije izmenjen.";
            return ok;
        }
    }
}

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
    public class TransportniNalogServis
    {
        private ITransportniNalogRepo _repo;
        private PoslovnaPravila _poslovnaPravila;
        public string? LastError { get; private set; }

        public TransportniNalogServis(ITransportniNalogRepo repo, PoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
            LastError = null;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveNaloge();
        }

        public DataSet PrikaziKreirane()
        {
            return _repo.DajKreiraneNaloge();
        }

        public DataSet PrikaziPoKlijentu(int klijentId)
        {
            return _repo.DajNalogePoKlijentu(klijentId);
        }

        public DataSet PrikaziPoID(int nalogId)
        {
            return _repo.DajNalogPoID(nalogId);
        }

        public bool Dodaj(int korisnikId, string naziv, string polaznaDestinacija, string krajnjaDestinacija, decimal nosivost)
        {
            // Validacija poslovnih pravila
            if (!_poslovnaPravila.KreirajNalog(korisnikId, naziv, polaznaDestinacija, krajnjaDestinacija, nosivost))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            TransportniNalog noviNalog = new TransportniNalog
            {
                Naziv = naziv,
                PolaznaDestinacija = polaznaDestinacija,
                KrajnjaDestinacija = krajnjaDestinacija,
                Nosivost = nosivost,
                KlijentID = korisnikId
            };

            var ok = _repo.NoviTransportniNalog(noviNalog);
            if (!ok) LastError = "Nalog nije kreiran.";
            return ok;
        }

        public bool Dodeli(int nalogId, int korisnikId, int vozacId, string registracija)
        {
            // 1. Provera da li je korisnik dispecer
            if (!_poslovnaPravila.ProveraDodeleDispecer(korisnikId))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            // 2. Provera da li vozac postoji
            if (!_poslovnaPravila.ProveraVozaca(vozacId))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            // 3. Provera nosivosti
            if (!_poslovnaPravila.ProveraNosivosti(nalogId, registracija))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            // 4. Dodela naloga
            var ok = _repo.DodeliNalog(nalogId, vozacId, registracija);
            if (!ok) LastError = "Nalog nije moguce dodeliti (mozda je vec dodeljen ili status nije 'Kreiran').";
            return ok;
        }
    }
}

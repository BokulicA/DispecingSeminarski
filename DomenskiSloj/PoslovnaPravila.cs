using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using System.Data;



namespace DomenskiSloj
{
    public class PoslovnaPravila
    {
        private IKorisnikRepo _repoKorisnik;
        private IVozacRepo _repoVozac;
        private IKamionRepo _repoKamion;
        private ITransportniNalogRepo _repoTransportniNalog;

        public PoslovnaPravila(
            IKorisnikRepo repoKorisnik,
            IVozacRepo repoVozac,
            IKamionRepo repoKamion,
            ITransportniNalogRepo repoTransportniNalog)
        {
            _repoKorisnik = repoKorisnik;
            _repoVozac = repoVozac;
            _repoKamion = repoKamion;
            _repoTransportniNalog = repoTransportniNalog;
        }

        /// Pravila:

        // 1) Novi nalog kreira SAMO klijent; naziv i destinacije su obavezni
        public bool KreirajNalog(int korisnikId, string naziv, string polaznaDestinacija, string krajnjaDestinacija, decimal nosivost)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                LastError = "Naziv ne može biti prazan.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(polaznaDestinacija))
            {
                LastError = "Polazna destinacija ne može biti prazna.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(krajnjaDestinacija))
            {
                LastError = "Krajnja destinacija ne može biti prazna.";
                return false;
            }

            if (nosivost <= 0)
            {
                LastError = "Nosivost mora biti veća od nule.";
                return false;
            }

            var ds = _repoKorisnik.DajKorisnikaPoID(korisnikId);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                LastError = "Nepostojeći korisnik.";
                return false;
            }

            var tipRaw = ds.Tables[0].Rows[0]["TipKorisnika"]?.ToString();
            var tip = tipRaw?.Trim();

            if (!string.Equals(tip, "Klijent", StringComparison.OrdinalIgnoreCase))
            {
                LastError = $"Samo klijent može kreirati nalog. Vaš tip: '{tipRaw}'.";
                return false;
            }

            LastError = "";
            return true;
        }

        // 2) Dodelu naloga može izvršiti SAMO dispečer
        public bool ProveraDodeleDispecer(int korisnikId)
        {
            var ds = _repoKorisnik.DajKorisnikaPoID(korisnikId);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                LastError = "Nepostojeći korisnik.";
                return false;
            }

            var tipRaw = ds.Tables[0].Rows[0]["TipKorisnika"]?.ToString();
            var tip = tipRaw?.Trim();

            if (!string.Equals(tip, "Dispečer", StringComparison.OrdinalIgnoreCase))
            {
                LastError = $"Samo dispečer može dodeliti nalog. Vaš tip: '{tipRaw}'.";
                return false;
            }

            LastError = "";
            return true;
        }

        // 3) Provera nosivosti: nosivost kamiona >= nosivost naloga
        public bool ProveraNosivosti(int nalogId, string registracija)
        {
            // Dohvatanje nosivosti naloga
            var dsNalog = _repoTransportniNalog.DajNalogPoID(nalogId);
            if (dsNalog == null || dsNalog.Tables.Count == 0 || dsNalog.Tables[0].Rows.Count == 0)
            {
                LastError = "Nalog ne postoji.";
                return false;
            }

            decimal nosivostNaloga = Convert.ToDecimal(dsNalog.Tables[0].Rows[0]["Nosivost"]);

            // Dohvatanje nosivosti kamiona
            var dsKamion = _repoKamion.DajKamionPoRegistraciji(registracija);
            if (dsKamion == null || dsKamion.Tables.Count == 0 || dsKamion.Tables[0].Rows.Count == 0)
            {
                LastError = "Kamion ne postoji.";
                return false;
            }

            decimal nosivostKamiona = Convert.ToDecimal(dsKamion.Tables[0].Rows[0]["Nosivost"]);

            if (nosivostKamiona < nosivostNaloga)
            {
                LastError = $"Nosivost kamiona ({nosivostKamiona} t) je manja od nosivosti naloga ({nosivostNaloga} t).";
                return false;
            }

            LastError = "";
            return true;
        }

        // 4) Provera da li vozač postoji
        public bool ProveraVozaca(int vozacId)
        {
            var ds = _repoVozac.DajVozacaPoID(vozacId);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                LastError = "Vozač ne postoji.";
                return false;
            }

            LastError = "";
            return true;
        }

        // 5) Validacija broja telefona vozača (mora biti tačno 10 cifara)
        public bool ValidacijaBrojaTelefona(string brojTelefona)
        {
            if (string.IsNullOrWhiteSpace(brojTelefona))
            {
                LastError = "Broj telefona ne može biti prazan.";
                return false;
            }

            string ociscen = brojTelefona.Trim();

            if (ociscen.Length != 10)
            {
                LastError = "Broj telefona mora imati tačno 10 cifara.";
                return false;
            }

            foreach (char c in ociscen)
            {
                if (!char.IsDigit(c))
                {
                    LastError = "Broj telefona mora sadržati samo cifre.";
                    return false;
                }
            }

            LastError = "";
            return true;
        }

        // 6) Validacija registracije kamiona (ne može biti prazna, min 3, max 20 karaktera)
        public bool ValidacijaRegistracije(string registracija)
        {
            if (string.IsNullOrWhiteSpace(registracija))
            {
                LastError = "Registracija ne može biti prazna.";
                return false;
            }

            string ociscena = registracija.Trim();

            if (ociscena.Length < 3)
            {
                LastError = "Registracija mora imati najmanje 3 karaktera.";
                return false;
            }

            if (ociscena.Length > 20)
            {
                LastError = "Registracija može imati najviše 20 karaktera.";
                return false;
            }

            LastError = "";
            return true;
        }

        public string LastError { get; private set; } = "";
    }
}


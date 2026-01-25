using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: Korisnik - Admin, Dispečer ili Klijent
    // Responsibility:
    // - Upisuje podatke korisnika (ime, prezime, korisničko ime, lozinka).
    // - Prati tip korisnika (Admin, Dispečer, Klijent).
    // Collaboration:
    // - Sa klasom TransportniNalog (klijent kreira naloge).
    public class Korisnik
    {
        private int _korisnikID;
        private string _ime;
        private string _prezime;
        private string _korisnickoIme;
        private string _lozinka;
        private string _tipKorisnika;

        public int KorisnikID
        {
            get { return _korisnikID; }
            set { _korisnikID = value; }
        }

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set { _korisnickoIme = value; }
        }

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        public string TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; }
        }
    }
}


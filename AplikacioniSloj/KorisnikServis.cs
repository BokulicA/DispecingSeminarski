using DomenskiSloj;
using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;
using System.Data;


namespace AplikacioniSloj
{
    public class KorisnikServis
    {
        private IKorisnikRepo _repo;
        public string? LastError { get; private set; }

        public KorisnikServis(IKorisnikRepo repo)
        {
            _repo = repo;
            LastError = null;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveKorisnike();
        }

        public DataSet PrikaziPoID(int korisnikId)
        {
            return _repo.DajKorisnikaPoID(korisnikId);
        }

        public Korisnik DajPoKorisnickomImenu(string korisnickoIme)
        {
            return _repo.DajKorisnikaPoKorisnickomImenu(korisnickoIme);
        }

        public bool Dodaj(Korisnik noviKorisnik)
        {
            var ok = _repo.NoviKorisnik(noviKorisnik);
            if (!ok) LastError = "Korisnik nije dodat.";
            return ok;
        }

        public bool Obrisi(int korisnikId)
        {
            var ok = _repo.ObrisiKorisnika(korisnikId);
            if (!ok) LastError = "Korisnik nije obrisan.";
            return ok;
        }

        public bool Izmeni(int korisnikId, Korisnik noviKorisnik)
        {
            var ok = _repo.IzmeniKorisnika(korisnikId, noviKorisnik);
            if (!ok) LastError = "Korisnik nije izmenjen.";
            return ok;
        }

        // Autentifikacija - use case
        public Korisnik Prijavi(string korisnickoIme, string lozinka)
        {
            var korisnik = _repo.DajKorisnikaPoKorisnickomImenu(korisnickoIme);

            if (korisnik == null)
            {
                LastError = "Korisnik ne postoji.";
                return null;
            }

            if (korisnik.Lozinka != lozinka)
            {
                LastError = "Pogresna lozinka.";
                return null;
            }

            LastError = null;
            return korisnik;
        }

        // Registracija - use case
        public bool Registruj(string ime, string prezime, string korisnickoIme, string lozinka, string potvrdaLozinke)
        {
            // Validacija - lozinke se moraju poklapati
            if (lozinka != potvrdaLozinke)
            {
                LastError = "Lozinke se ne poklapaju.";
                return false;
            }

            // Provera da li korisnicko ime vec postoji
            var postojeciKorisnik = _repo.DajKorisnikaPoKorisnickomImenu(korisnickoIme);
            if (postojeciKorisnik != null)
            {
                LastError = "Korisnicko ime je vec zauzeto.";
                return false;
            }

            // Kreiranje novog korisnika
            Korisnik noviKorisnik = new Korisnik
            {
                Ime = ime,
                Prezime = prezime,
                KorisnickoIme = korisnickoIme,
                Lozinka = lozinka,
                TipKorisnika = "Klijent"
            };

            var ok = _repo.NoviKorisnik(noviKorisnik);
            if (!ok)
            {
                LastError = "Registracija nije uspela.";
                return false;
            }

            LastError = null;
            return true;
        }
    }
}

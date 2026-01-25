using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: TransportniNalog - Glavni entitet za transport poslove
    // Responsibility:
    // - Čuva podatke o nalogu (destinacije, nosivost, status).
    // - Prati dodeljenog vozača i kamion.
    // - Prati klijenta koji je kreirao nalog.
    // Collaboration:
    // - Sa klasom Korisnik (klijent).
    // - Sa klasom Vozac (dodeljeni vozač).
    // - Sa klasom Kamion (dodeljen kamion).
    public class TransportniNalog
    {
        private int _nalogID;
        private string _naziv;
        private string _polaznaDestinacija;
        private string _krajnjaDestinacija;
        private decimal _nosivost;
        private int _klijentID;
        private int? _vozacID;
        private string _registracija;
        private DateTime _datumKreiranja;
        private string _status;

        public int NalogID
        {
            get { return _nalogID; }
            set { _nalogID = value; }
        }

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }

        public string PolaznaDestinacija
        {
            get { return _polaznaDestinacija; }
            set { _polaznaDestinacija = value; }
        }

        public string KrajnjaDestinacija
        {
            get { return _krajnjaDestinacija; }
            set { _krajnjaDestinacija = value; }
        }

        public decimal Nosivost
        {
            get { return _nosivost; }
            set { _nosivost = value; }
        }

        public int KlijentID
        {
            get { return _klijentID; }
            set { _klijentID = value; }
        }

        public int? VozacID
        {
            get { return _vozacID; }
            set { _vozacID = value; }
        }

        public string Registracija
        {
            get { return _registracija; }
            set { _registracija = value; }
        }

        public DateTime DatumKreiranja
        {
            get { return _datumKreiranja; }
            set { _datumKreiranja = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
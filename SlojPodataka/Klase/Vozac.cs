using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: Vozac - Vozač koji izvršava transportne naloge
    // Responsibility:
    // - Čuva osnovne podatke vozača (ime, prezime, telefon).
    // Collaboration:
    // - Sa klasom TransportniNalog (dodeljuje se nalozima).
    public class Vozac
    {
        private int _vozacID;
        private string _ime;
        private string _prezime;
        private string _brojTelefona;

        public int VozacID
        {
            get { return _vozacID; }
            set { _vozacID = value; }
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

        public string BrojTelefona
        {
            get { return _brojTelefona; }
            set { _brojTelefona = value; }
        }
    }
}


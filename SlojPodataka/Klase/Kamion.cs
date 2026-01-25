using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: Kamion - Vozilo za transport
    // Responsibility:
    // - Čuva podatke o kamionu (registracija, marka, nosivost).
    // Collaboration:
    // - Sa klasom TransportniNalog (dodeljuje se nalozima).
    public class Kamion
    {
        private string _registracija;
        private string _marka;
        private decimal _nosivost;

        public string Registracija
        {
            get { return _registracija; }
            set { _registracija = value; }
        }

        public string Marka
        {
            get { return _marka; }
            set { _marka = value; }
        }

        public decimal Nosivost
        {
            get { return _nosivost; }
            set { _nosivost = value; }
        }
    }
}

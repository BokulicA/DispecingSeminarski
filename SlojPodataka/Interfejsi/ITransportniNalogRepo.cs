using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Klase;


namespace SlojPodataka.Interfejsi
{
    public interface ITransportniNalogRepo
    {
        DataSet DajSveNaloge();
        DataSet DajKreiraneNaloge();
        DataSet DajNalogePoKlijentu(int KlijentID);
        DataSet DajNalogPoID(int NalogID);
        bool NoviTransportniNalog(TransportniNalog objNoviNalog);
        bool DodeliNalog(int NalogID, int VozacID, string Registracija);
    }
}

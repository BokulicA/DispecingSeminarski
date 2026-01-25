using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Klase;


namespace SlojPodataka.Interfejsi
{
    public interface IVozacRepo
    {
        DataSet DajSveVozace();
        DataSet DajVozacaPoID(int VozacID);
        bool NoviVozac(Vozac objNoviVozac);
        bool ObrisiVozaca(int VozacID);
        bool IzmeniVozaca(int VozacID, Vozac objNoviVozac);
    }
}

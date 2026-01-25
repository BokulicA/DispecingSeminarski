using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Klase;


namespace SlojPodataka.Interfejsi
{
    public interface IKamionRepo
    {
        DataSet DajSveKamione();
        DataSet DajKamionPoRegistraciji(string Registracija);
        bool NoviKamion(Kamion objNoviKamion);
        bool ObrisiKamion(string Registracija);
        bool IzmeniKamion(string Registracija, Kamion objNoviKamion);
    }
}

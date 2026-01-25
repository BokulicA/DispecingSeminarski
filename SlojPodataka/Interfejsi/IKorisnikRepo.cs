using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Klase;


namespace SlojPodataka.Interfejsi
{
    public interface IKorisnikRepo
    {
        DataSet DajSveKorisnike();
        DataSet DajKorisnikaPoID(int KorisnikID);
        bool NoviKorisnik(Korisnik objNoviKorisnik);
        bool ObrisiKorisnika(int KorisnikID);
        bool IzmeniKorisnika(int KorisnikID, Korisnik objNoviKorisnik);
        Korisnik DajKorisnikaPoKorisnickomImenu(string KorisnickoIme);
    }
}

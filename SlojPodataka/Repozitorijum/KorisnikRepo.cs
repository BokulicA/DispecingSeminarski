using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka.Repozitorijum
{
    public class KorisnikRepo : IKorisnikRepo
    {
        private string _stringKonekcije;

        public KorisnikRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveKorisnike()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveKorisnike", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajKorisnikaPoID(int KorisnikID)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajKorisnikaPoID", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = KorisnikID;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool NoviKorisnik(Korisnik objNoviKorisnik)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviKorisnik", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objNoviKorisnik.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objNoviKorisnik.Prezime;
            Komanda.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = objNoviKorisnik.KorisnickoIme;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objNoviKorisnik.Lozinka;
            Komanda.Parameters.Add("@TipKorisnika", SqlDbType.NVarChar).Value = objNoviKorisnik.TipKorisnika;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool ObrisiKorisnika(int KorisnikID)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("ObrisiKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = KorisnikID;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool IzmeniKorisnika(int KorisnikID, Korisnik objNoviKorisnik)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("IzmeniKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = KorisnikID;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objNoviKorisnik.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objNoviKorisnik.Prezime;
            Komanda.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = objNoviKorisnik.KorisnickoIme;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objNoviKorisnik.Lozinka;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public Korisnik DajKorisnikaPoKorisnickomImenu(string KorisnickoIme)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {
                Veza.Open();
                SqlCommand Komanda = new SqlCommand("DajKorisnikaPoKorisnickomImenu", Veza);
                Komanda.CommandType = CommandType.StoredProcedure;
                Komanda.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = KorisnickoIme;

                using (SqlDataReader Reader = Komanda.ExecuteReader())
                {
                    if (Reader.Read())
                    {
                        return new Korisnik
                        {
                            KorisnikID = Convert.ToInt32(Reader["KorisnikID"]),
                            Ime = Reader["Ime"].ToString(),
                            Prezime = Reader["Prezime"].ToString(),
                            KorisnickoIme = Reader["KorisnickoIme"]?.ToString(),
                            Lozinka = Reader["Lozinka"].ToString(),
                            TipKorisnika = Reader["TipKorisnika"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}

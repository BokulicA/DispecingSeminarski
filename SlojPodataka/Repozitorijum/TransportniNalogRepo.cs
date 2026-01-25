using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka.Repozitorijum
{
    public class TransportniNalogRepo : ITransportniNalogRepo
    {
        private string _stringKonekcije;

        public TransportniNalogRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveNaloge()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveNaloge", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajKreiraneNaloge()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajKreiraneNaloge", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajNalogePoKlijentu(int KlijentID)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajNalogePoKlijentu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@KlijentID", SqlDbType.Int).Value = KlijentID;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajNalogPoID(int NalogID)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajNalogPoID", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@NalogID", SqlDbType.Int).Value = NalogID;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool NoviTransportniNalog(TransportniNalog objNoviNalog)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviTransportniNalog", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Naziv", SqlDbType.NVarChar).Value = objNoviNalog.Naziv;
            Komanda.Parameters.Add("@PolaznaDestinacija", SqlDbType.NVarChar).Value = objNoviNalog.PolaznaDestinacija;
            Komanda.Parameters.Add("@KrajnjaDestinacija", SqlDbType.NVarChar).Value = objNoviNalog.KrajnjaDestinacija;
            Komanda.Parameters.Add("@Nosivost", SqlDbType.Decimal).Value = objNoviNalog.Nosivost;
            Komanda.Parameters.Add("@KlijentID", SqlDbType.Int).Value = objNoviNalog.KlijentID;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool DodeliNalog(int NalogID, int VozacID, string Registracija)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DodeliNalog", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@NalogID", SqlDbType.Int).Value = NalogID;
            Komanda.Parameters.Add("@VozacID", SqlDbType.Int).Value = VozacID;
            Komanda.Parameters.Add("@Registracija", SqlDbType.NVarChar).Value = Registracija;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
    }
}


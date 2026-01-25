using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka.Repozitorijum
{
    public class KamionRepo : IKamionRepo
    {
        private string _stringKonekcije;

        public KamionRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveKamione()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveKamione", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajKamionPoRegistraciji(string Registracija)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajKamionPoRegistraciji", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Registracija", SqlDbType.NVarChar).Value = Registracija;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool NoviKamion(Kamion objNoviKamion)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviKamion", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Registracija", SqlDbType.NVarChar).Value = objNoviKamion.Registracija;
            Komanda.Parameters.Add("@Marka", SqlDbType.NVarChar).Value = objNoviKamion.Marka;
            Komanda.Parameters.Add("@Nosivost", SqlDbType.Decimal).Value = objNoviKamion.Nosivost;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool ObrisiKamion(string Registracija)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("ObrisiKamion", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Registracija", SqlDbType.NVarChar).Value = Registracija;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool IzmeniKamion(string Registracija, Kamion objNoviKamion)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("IzmeniKamion", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Registracija", SqlDbType.NVarChar).Value = Registracija;
            Komanda.Parameters.Add("@Marka", SqlDbType.NVarChar).Value = objNoviKamion.Marka;
            Komanda.Parameters.Add("@Nosivost", SqlDbType.Decimal).Value = objNoviKamion.Nosivost;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
    }
}

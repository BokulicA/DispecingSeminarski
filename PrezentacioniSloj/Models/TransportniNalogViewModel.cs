namespace PrezentacioniSloj.Models
{
    public class TransportniNalogViewModel
    {
        public int NalogID { get; set; }
        public string Naziv { get; set; }
        public string PolaznaDestinacija { get; set; }
        public string KrajnjaDestinacija { get; set; }
        public decimal Nosivost { get; set; }
        public string Klijent { get; set; }
        public string Vozac { get; set; }
        public string Registracija { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Status { get; set; }
    }
}

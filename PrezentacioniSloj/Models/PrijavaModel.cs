using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.Models
{
    public class PrijavaModel
    {
        [Required(ErrorMessage = "Korisnicko ime je obavezno.")]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }
    }
}

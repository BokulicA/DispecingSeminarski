using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.Models
{
    public class DodeliNalogModel
    {
        [Required]
        public int NalogID { get; set; }

        [Required(ErrorMessage = "Morate izabrati vozaca.")]
        [Display(Name = "Vozac")]
        public int VozacID { get; set; }

        [Required(ErrorMessage = "Morate izabrati kamion.")]
        [Display(Name = "Kamion")]
        public string Registracija { get; set; }
    }
}

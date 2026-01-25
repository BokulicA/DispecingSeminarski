using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.Models
{
    public class NoviVozacModel
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(30, ErrorMessage = "Ime moze imati maksimalno 30 karaktera.")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(40, ErrorMessage = "Prezime moze imati maksimalno 40 karaktera.")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Broj telefona mora imati tacno 10 cifara.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Broj telefona mora sadrzati samo cifre.")]
        [Display(Name = "Broj telefona")]
        public string BrojTelefona { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.Models
{
    public class RegistracijaModel
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(30, ErrorMessage = "Ime moze imati maksimalno 30 karaktera.")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(40, ErrorMessage = "Prezime moze imati maksimalno 40 karaktera.")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Korisnicko ime je obavezno.")]
        [StringLength(20, ErrorMessage = "Korisnicko ime moze imati maksimalno 20 karaktera.")]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Lozinka mora imati izmedju 6 i 30 karaktera.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Potvrda lozinke je obavezna.")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrda lozinke")]
        [Compare("Lozinka", ErrorMessage = "Lozinke se ne poklapaju.")]
        public string PotvrdaLozinke { get; set; }
    }
}

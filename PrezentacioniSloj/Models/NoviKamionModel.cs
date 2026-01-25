using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.Models
{
    public class NoviKamionModel
    {
        [Required(ErrorMessage = "Registracija je obavezna.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Registracija mora imati izmedju 3 i 20 karaktera.")]
        [Display(Name = "Registracija")]
        public string Registracija { get; set; }

        [Required(ErrorMessage = "Marka je obavezna.")]
        [StringLength(50, ErrorMessage = "Marka moze imati maksimalno 50 karaktera.")]
        [Display(Name = "Marka")]
        public string Marka { get; set; }

        [Required(ErrorMessage = "Nosivost je obavezna.")]
        [Range(0.1, 100.0, ErrorMessage = "Nosivost mora biti izmedju 0.1 i 100 tona.")]
        [Display(Name = "Nosivost (t)")]
        public decimal Nosivost { get; set; }
    }
}

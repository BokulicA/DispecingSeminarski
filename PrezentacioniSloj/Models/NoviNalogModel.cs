using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.Models
{
    public class NoviNalogModel
    {
        [Required(ErrorMessage = "Naziv je obavezan.")]
        [StringLength(100, ErrorMessage = "Naziv moze imati maksimalno 100 karaktera.")]
        [Display(Name = "Naziv")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Polazna destinacija je obavezna.")]
        [StringLength(100, ErrorMessage = "Polazna destinacija moze imati maksimalno 100 karaktera.")]
        [Display(Name = "Polazna destinacija")]
        public string PolaznaDestinacija { get; set; }

        [Required(ErrorMessage = "Krajnja destinacija je obavezna.")]
        [StringLength(100, ErrorMessage = "Krajnja destinacija moze imati maksimalno 100 karaktera.")]
        [Display(Name = "Krajnja destinacija")]
        public string KrajnjaDestinacija { get; set; }

        [Required(ErrorMessage = "Nosivost je obavezna.")]
        [Range(0.1, 100.0, ErrorMessage = "Nosivost mora biti izmedju 0.1 i 100 tona.")]
        [Display(Name = "Nosivost (t)")]
        public decimal Nosivost { get; set; }
    }
}

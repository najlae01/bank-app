using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank_app.Models
{
    public class Compte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        public string nom { get; set; }


        // Relashionships
        public List<Mouvement> mouvements { get; set; } = new List<Mouvement>();
    }
}

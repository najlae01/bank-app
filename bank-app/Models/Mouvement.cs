using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bank_app.Models
{
    public class Mouvement
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Account ID")]
        [Column("compte_id")]
        public int compte_id { get; set; }

        [ForeignKey("compte_id")]
        [Display(Name = "Account Holder Name")]
        public Compte compte { get; set; }

        [Display(Name = "Transaction Amount")]
        public double montant { get; set; }

        [Display(Name = "Date")]
        public DateTime date_mnt { get; set; }
    }
}

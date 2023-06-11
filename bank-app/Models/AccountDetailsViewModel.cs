namespace bank_app.Models
{
    public class AccountDetailsViewModel
    {
        public IEnumerable<Mouvement> Mouvements { get; set; }
        public Compte Compte { get; set; }
    }
}

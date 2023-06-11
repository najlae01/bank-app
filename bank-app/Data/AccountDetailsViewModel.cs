using bank_app.Models;

namespace bank_app.Data
{
    public class AccountDetailsViewModel
    {
        public IEnumerable<Mouvement> Mouvements { get; set; }
        public Compte Compte { get; set; }
    }

}

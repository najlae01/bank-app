using bank_app.Models;

namespace bank_app.Data.Services
{
    public interface IComptesService
    {
        Task<IEnumerable<Compte>> GetAll();

        Task<Compte> GetById(int id);

        Task Add(Compte compte);

        Compte Update(int id, Compte newCompte);

        void Delete(int id);
    }
}

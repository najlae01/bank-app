using bank_app.Models;

namespace bank_app.Data.Services
{
    public interface IComptesService
    {
        Task<IEnumerable<Compte>> GetAll();

        Task<Compte> GetById(int id);

        Task Add(Compte compte);

        Task<Compte> Update(int id, Compte newCompte);

        Task Delete(int id);
    }
}

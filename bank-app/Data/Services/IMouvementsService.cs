using bank_app.Models;

namespace bank_app.Data.Services
{
    public interface IMouvementsService
    {
        Task<IEnumerable<Mouvement>> GetAll();

        Mouvement GetById(int id);

        Task Add(Mouvement mouvement);

        Mouvement Update(int id, Compte newMouvement);

        void Delete(int id);
    }
}

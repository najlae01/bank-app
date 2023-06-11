using bank_app.Models;

namespace bank_app.Data.Services
{
    public interface IMouvementsService
    {
        Task<IEnumerable<Mouvement>> GetAll();

        Mouvement GetById(int id);

        Task Add(Mouvement mouvement);

        Task<Mouvement> Update (int id, Mouvement newMouvement);

        void Delete(int id);
    }
}

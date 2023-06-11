using bank_app.Models;

namespace bank_app.Data.Services
{
    public interface IMouvementsService
    {
        Task<IEnumerable<Mouvement>> GetAll();

        Task<Mouvement> GetById(int id);

        Task Add(Mouvement mouvement);

        Task<Mouvement> Update (int id, Mouvement newMouvement);

        Task Delete(int id);
    }
}

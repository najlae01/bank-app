using bank_app.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_app.Data.Services
{
    public class MouvementsServices : IMouvementsService
    {

        private readonly AppDBContext _dbContext;

        public MouvementsServices(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Mouvement mouvement)
        {
            await _dbContext.Mouvements.AddAsync(mouvement);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Mouvement>> GetAll()
        {
            var result = await _dbContext.Mouvements.ToListAsync();
            return result;
        }

        public Mouvement GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Mouvement> Update(int id, Mouvement newMouvement)
        {
            _dbContext.Mouvements.Update(newMouvement);
            _dbContext.SaveChanges();
            return newMouvement;
        }

        public void DisableForeignKeys()
        {
            _dbContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 0;");
        }

        public void EnableForeignKeys()
        {
            _dbContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 1;");
        }
    }
}

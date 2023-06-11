using bank_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task Delete(int id)
        {
            var mouvement = await _dbContext.Mouvements.FirstOrDefaultAsync(n => n.id == id);
            //to implement
            if (mouvement == null)
            {
                return;
            }

            // Remove the mouvement from the associated account
            var compte = await _dbContext.Comptes.FirstOrDefaultAsync(c => c.id == mouvement.compte_id);
            if (compte != null)
            {
                compte.mouvements.Remove(mouvement);
            }

            _dbContext.Mouvements.Remove(mouvement);

            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Mouvement>> GetAll()
        {
            // Fetching Mouvement objects from the database including the related Compte entity
            var mouvements = await _dbContext.Mouvements.Include(m => m.compte).ToListAsync();
            return mouvements;
        }

        public async Task<Mouvement> GetById(int id)
        {
            var result = await _dbContext.Mouvements.FirstOrDefaultAsync(n => n.id == id);
            return result;
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

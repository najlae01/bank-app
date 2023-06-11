using bank_app.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_app.Data.Services
{
    public class ComptesServices : IComptesService
    {

        private readonly AppDBContext _dbContext;

        public ComptesServices(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Compte compte)
        {
            await _dbContext.Comptes.AddAsync(compte);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {

        }

        public async Task<IEnumerable<Compte>> GetAll()
        {
            var comptes = await _dbContext.Comptes.Include(c => c.mouvements).ToListAsync();
            return comptes;
        }

        public async Task<Compte> GetById(int id)
        {
            var result = await _dbContext.Comptes.FirstOrDefaultAsync(n => n.id == id);
            return result;
        }

        public Compte Update(int id, Compte newCompte)
        {
            return null;
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

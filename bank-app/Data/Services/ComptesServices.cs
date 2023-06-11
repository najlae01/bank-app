using bank_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task Delete(int id)
        {
            var compte = await _dbContext.Comptes.Include(c => c.mouvements).FirstOrDefaultAsync(c => c.id == id);

            if (compte == null)
            {
                // Handle the case when the account doesn't exist
                return;
            }

            // Remove the associated transactions
            _dbContext.Mouvements.RemoveRange(compte.mouvements);

            // Remove the account
            _dbContext.Comptes.Remove(compte);

            // Save the changes to the database
            _dbContext.SaveChanges();
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

        public async Task<Compte> Update(int id, Compte newCompte)
        {
            var existingCompte = await _dbContext.Comptes.FindAsync(id);
            if (existingCompte == null)
            {
                // Handle the case when the compte does not exist
                return null;
            }

            // Update the properties of the existingCompte with the newCompte
            existingCompte.nom = newCompte.nom;
            existingCompte.mouvements = newCompte.mouvements;

            _dbContext.SaveChanges();
            return existingCompte;
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

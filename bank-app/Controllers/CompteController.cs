using bank_app.Data;
using bank_app.Data.Services;
using bank_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace bank_app.Controllers
{
    public class CompteController : Controller
    {
        private readonly IComptesService _service;
        private readonly IMouvementsService _mouvementsService;

        public CompteController(IComptesService service, IMouvementsService mouvementsService)
        {
            _service = service;
            _mouvementsService = mouvementsService;
        }

        public async Task<IActionResult> Index()
        {
            var allComptes = await _service.GetAll();
            return View(allComptes);
        }

        // Get: Compte/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("nom")] Compte compte)
        {
            /*if(!ModelState.IsValid)
            {
                return View(compte);
            }*/
            await _service.Add(compte);
            return RedirectToAction(nameof(Index));
        }

        // Get: Compte/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var compteDetails = await _service.GetById(id);

            if (compteDetails == null) return View("NotFound");

            var mouvements = await _mouvementsService.GetAll();
            var mouvementsByAccount = mouvements.Where(m => m.compte_id == id);


            var viewModel = new Tuple<IEnumerable<Mouvement>, Compte>(mouvementsByAccount, compteDetails);

            return View(viewModel);
        }


        // GET: Compte/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var compteDetails = await _service.GetById(id);

            if (compteDetails == null) return View("NotFound");

            var viewModel = new AccountDetailsViewModel
            {
                Compte = compteDetails
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Compte")] AccountDetailsViewModel viewModel)
        {
             var compte = await _service.GetById(id);

                if (compte == null) return View("NotFound");

                compte.nom = viewModel.Compte.nom;

                // Save the updated account details
                await _service.Update(id, compte);

                return RedirectToAction("Details", new { id = compte.id });
        }


        // GET: Compte/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var compteDetails = await _service.GetById(id);

            if (compteDetails == null) return View("NotFound");

            var mouvements = await _mouvementsService.GetAll();
            var mouvementsByAccount = mouvements.Where(m => m.compte_id == id);


            var viewModel = new Tuple<IEnumerable<Mouvement>, Compte>(mouvementsByAccount, compteDetails);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compteDetails = await _service.GetById(id);

            if (compteDetails == null) return View("NotFound");
            
            await _service.Delete(id);

            return RedirectToAction("Index", "Compte");
        }

    }
}

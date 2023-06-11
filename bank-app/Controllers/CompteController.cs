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

            if (compteDetails == null) return View("Empty");

            var mouvements = await _mouvementsService.GetAll();
            var mouvementsByAccount = mouvements.Where(m => m.compte_id == id);


            var viewModel = new Tuple<IEnumerable<Mouvement>, Compte>(mouvementsByAccount, compteDetails);

            return View(viewModel);
        }
    }
}

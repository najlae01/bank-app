using bank_app.Data;
using bank_app.Data.Services;
using bank_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank_app.Controllers
{
    public class MouvementController : Controller
    {
        private readonly IMouvementsService _service;

        private readonly IComptesService _comptesService;

        public MouvementController(IMouvementsService service, IComptesService comptesService)
        {
            _service = service;
            _comptesService = comptesService;
        }
        public async Task<IActionResult> Index()
        {
            var allMouvements = await _service.GetAll();
            return View(allMouvements);
        }

        // Get: Mouvement/AddTransaction
        public async Task<IActionResult> AddTransaction(int id)
        {
            var compteDetails = await _comptesService.GetById(id);

            ViewBag.id = id;

            if (compteDetails == null) return View("Empty");
            var mouvement = new Mouvement { compte_id = id };

            ViewBag.compte_id = mouvement.compte_id;
            return View(mouvement);
        }


        [HttpPost]
        public async Task<IActionResult> AddTransaction([Bind("montant", "compte_id")] Mouvement mouvement)
        {
            mouvement.date_mnt = DateTime.Now;

            ViewBag.mouvement = mouvement;
            await _service.Add(mouvement);
            return RedirectToAction("Index", "Compte");
        }
    }
}

using bank_app.Data;
using bank_app.Data.Services;
using bank_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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


            if (compteDetails == null) return View("NotFound");
            var mouvement = new Mouvement
            {
                compte_id = id
            };


            ViewBag.compte_id = mouvement.compte_id;
            return View(mouvement);
        }


        [HttpPost]
        public async Task<IActionResult> AddTransaction([Bind("montant, compte_id")] Mouvement mouvement)
        {
            mouvement.date_mnt = DateTime.Now;

            // Retrieve the associated compte
            var compte = await _comptesService.GetById(mouvement.compte_id);

            if (compte == null)
            {
                // Handle the case when the associated compte is not found
                return View("NotFound");
            }

            ViewBag.mouvement = mouvement;

            // Set the mouvement's compte property to the retrieved compte
            mouvement.compte = compte;

            await _service.Add(mouvement);

            // Add the mouvement to the compte's collection of mouvements
            compte.mouvements.Add(mouvement);

            // Update the compte in the database
            //await _comptesService.Update(mouvement.compte_id, compte);

            return RedirectToAction("Index", "Compte");
        }



        // GET: Mouvement/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var mouvementDetails = await _service.GetById(id);

            ViewBag.id = id;

            if (mouvementDetails == null) return View("NotFound");

            var compteDetails = await _comptesService.GetById(mouvementDetails.compte_id);

            if (compteDetails == null) return View("NotFound");

            var viewModel = new Tuple<Mouvement, Compte>(mouvementDetails, compteDetails);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mouvementDetails = await _service.GetById(id);

            if (mouvementDetails == null) return View("NotFound");

            var compte_id = mouvementDetails.compte_id;

            await _service.Delete(id);

            return RedirectToAction("Details", "Compte", new { id = compte_id });
        }
    }
}

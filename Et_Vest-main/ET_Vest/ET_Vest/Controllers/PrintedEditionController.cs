using ET_Vest.Data;
using ET_Vest.Data.ViewModels;
using ET_Vest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;

namespace ET_Vest.Controllers
{
    public class PrintedEditionController : Controller
    {
        private readonly ApplicationDbContext context;

        public PrintedEditionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var printedEdition = context.PrintedEditions
            .Include(m => m.PrintEditionProviders)
            .Include(m => m.Requests)
            .ToList();

            return View(printedEdition);
        }

        //Add PrintedEdition
        //   [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            ViewBag.Providers = context.PrintedEditionProviders.Include
                (pp => pp.Provider).ToList();
            ViewBag.Requests = context.Requests.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(PrintedEdition printedEdition)
        {
            context.PrintedEditions.Add(printedEdition);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Printed Editions
        // [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var printedEdition = context.PrintedEditions.Find(id);

            if (printedEdition == null)
            {
                return NotFound();
            }

            var printedEditionProvider = context.PrintedEditionProviders
                    .Include(pe => pe.Provider)
                    .FirstOrDefault(pe => pe.PrintedEditionId == id);

            var viewModel = new PrintedEditionProviderViewModel
            {
                PrintedEdition = printedEdition,
                PrintedEditionProvider = printedEditionProvider
            };
            ViewBag.Providers = new SelectList(context.Providers, "ProviderId", "Name");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PrintedEditionProviderViewModel viewModel)
        {
            //update edition details
            context.PrintedEditions.Update(viewModel.PrintedEdition);
            //update printed edition provider details
         //   context.Entry(viewModel.PrintedEditionProvider).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        //  [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var printedEditions = context.PrintedEditions.Find(id);

            if (printedEditions == null)
            {
                return NotFound();
            }

            context.PrintedEditions.Remove(printedEditions);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using ET_Vest.Data;
using ET_Vest.Data.ViewModels;
using ET_Vest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace ET_Vest.Controllers
{
    public class ProviderController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProviderController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var provider = context.Providers
            .Include(p => p.PrintEditionProviders)
            .ThenInclude(p => p.PrintedEdition)
            .ToList();

            return View(provider);
        }

        //Add Provider
        [Authorize(Roles = "Owner")]

        public IActionResult Add()
        {
            ViewBag.PrintedEditions = context.PrintedEditionProviders.Include
                (ma => ma.PrintedEdition).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Provider provider)
        {
            context.Providers.Add(provider);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Provider
        [Authorize(Roles = "Owner")]

        public IActionResult Edit(int id)
        {
            var provider = context.Providers.Find(id);

            if (provider == null)
            {
                return NotFound();
            }

            var printedEditionProvider = context.PrintedEditionProviders
          .Include(pe => pe.PrintedEdition)
          .FirstOrDefault(pe => pe.ProviderId == id);

            var viewModel = new PrintedEditionProviderViewModel
            {
                Provider = provider,
                PrintedEditionProvider = printedEditionProvider
            };
            ViewBag.PrintedEditions = new SelectList(context.PrintedEditions, "PrintedEditionId", "Title");
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(PrintedEditionProviderViewModel viewModel)
        {
            //update provider details
            context.Providers.Update(viewModel.Provider);
            //update printed edition provider details
          //  context.Entry(viewModel.PrintedEditionProvider).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public IActionResult Delete(int id)
        {
            var provider = context.Providers.Find(id);

            if (provider == null)
            {
                return NotFound();
            }

            context.Providers.Remove(provider);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

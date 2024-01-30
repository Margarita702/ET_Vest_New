using ET_Vest.Data;
using ET_Vest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var printedEdition = context.PrintedEditions
                .Include(m => m.PrintEditionProviders)
                .Include(m => m.Requests)
                .FirstOrDefault(m => m.PrintedEditionId == id);
            if (printedEdition == null)
            {
                return NotFound();
            }

            ViewBag.Providers = context.PrintedEditionProviders.Include
                (ma => ma.Provider).ToList();
            ViewBag.Requests = context.Requests.ToList();

            return View(printedEdition);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PrintedEdition printedEdition)
        {
            context.PrintedEditions.Update(printedEdition);
            context.SaveChanges();
            return RedirectToAction("Index");

            //  return View(printedEdition);
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

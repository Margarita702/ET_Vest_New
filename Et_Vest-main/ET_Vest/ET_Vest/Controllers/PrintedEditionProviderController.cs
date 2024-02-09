using ET_Vest.Data;
using ET_Vest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ET_Vest.Controllers
{
    [Authorize(Roles = "Owner")]
    public class PrintedEditionProviderController : Controller
    {
        private readonly ApplicationDbContext context;

        public PrintedEditionProviderController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var printedEditionProvider = context.PrintedEditionProviders
            .Include(m => m.PrintedEdition)
            .Include(m => m.Provider)
            .ToList();

            return View(printedEditionProvider);
        }

        //Add PrintedEditionProvider
        public IActionResult Add()
        {
            ViewBag.PrintedEditions = context.PrintedEditions.ToList();
            ViewBag.Providers = context.Providers.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(PrintedEditionProvider printedEditionProvider)
        {
            context.PrintedEditionProviders.Add(printedEditionProvider);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

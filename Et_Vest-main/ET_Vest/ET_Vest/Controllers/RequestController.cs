using ET_Vest.Data;
using ET_Vest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ET_Vest.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext context;

        public RequestController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var requests = context.Requests
           .Include(t => t.TradeObject)
           .Include(t => t.PrintedEdition)
           .Include(t => t.Provider)
           .ToList();

            return View(requests);
        }

        public IActionResult Add()
        {
            ViewBag.TradeObjects = context.TradeObjects.ToList();
            ViewBag.PrintedEditions = context.PrintedEditions.ToList();
            ViewBag.Providers = context.Providers.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Request request)
        {
            request.Status = "Изчакваща";
            context.Requests.Add(request);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var request = context.Requests
                .Include(m => m.TradeObject)
                .Include(m => m.PrintedEdition)
                .Include(m => m.Provider)
                .FirstOrDefault(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            ViewBag.TradeObjects = context.TradeObjects.ToList();
            ViewBag.PrintedEditions = context.PrintedEditions.ToList();
            ViewBag.Providers = context.Providers.ToList();

            return View(request);
        }

        [HttpPost]
        public IActionResult Edit(Request request)
        {

            context.Requests.Update(request);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var request = context.Requests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            context.Requests.Remove(request);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SentToOwner(int id)
        {
            var request = context.Requests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            request.Status = "Изпратена към собственик"; // Маркиране на съобщението като прочетено

            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SentToProvider(int id)
        {
            var request = context.Requests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            request.Status = "Изпратена към доставчик"; // Маркиране на съобщението като прочетено

            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DoneRequest(int id)
        {
            var request = context.Requests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            request.Status = "Изпълнена"; // Маркиране на съобщението като прочетено

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

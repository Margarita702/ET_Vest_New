using ET_Vest.Data;
using ET_Vest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ET_Vest.Controllers
{
    public class TradeObjectController : Controller
    {
        private readonly ApplicationDbContext context;

        public TradeObjectController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var tradeObject = context.TradeObjects
           .Include(r => r.Requests)
           .ToList();

            return View(tradeObject);
        }

        ////Add TradeObject
        //[Authorize(Roles = "Admin")]

        public IActionResult Add()
        {
            ViewBag.Requests = context.Requests.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(TradeObject tradeObject)
        {
            context.TradeObjects.Add(tradeObject);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        ////Update TradeObject
        //[Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var tradeObject = context.TradeObjects
                .Include(m => m.Requests)
                .FirstOrDefault(m => m.Id == id);
            if (tradeObject == null)
            {
                return NotFound();
            }

            ViewBag.Requests = context.Requests.ToList();

            return View(tradeObject);
        }

        [HttpPost]
        public IActionResult Edit(TradeObject tradeObject)
        {
          //  if (ModelState.IsValid)
           // {

                context.TradeObjects.Update(tradeObject);
                context.SaveChanges();
                return RedirectToAction("Index");
         //   }
         //   return View(tradeObject);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var tradeObject = context.TradeObjects.Find(id);

            if (tradeObject == null)
            {
                return NotFound();
            }

            context.TradeObjects.Remove(tradeObject);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

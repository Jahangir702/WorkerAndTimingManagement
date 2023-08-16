using Class_03_Practise_01.Models;
using Class_03_Practise_01.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Class_03_Practise_01.Controllers
{
    public class WorkLogsController : Controller
    {
        readonly WorkerDbContext db;

        public WorkLogsController(WorkerDbContext db)
        {
            this.db = db;

        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            var data = await db.WorkLogs
                 .Include(x => x.Worker).OrderBy(x => x.WorkerId)
                 .Select(w => new WorkLogViewModel
                 {
                     WorkLogId = w.WorkLogId,
                     WorkerId = w.WorkerId,
                     StartDate = w.StartDate,
                     EndDate = w.EndDate,
                     WorkerName = w.Worker != null ? w.Worker.WorkerName : null,
                     PayRate = w.Worker != null ? w.Worker.PayRate : null
                 })
                 .ToPagedListAsync(pg, 5);
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Workers = await db.Workers.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkLogInputModel model)
        {
            if (ModelState.IsValid)
            {
                var l = new WorkLog()
                {
                    WorkerId = model.WorkerId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };
                await db.WorkLogs.AddAsync(l);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Workers");

            }
            ViewBag.Workers = await db.Workers.ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var l = await db.WorkLogs.FirstOrDefaultAsync(x => x.WorkLogId == id);
            ViewBag.Workers = await db.Workers.ToListAsync();
            return View(l);
        }
        public ActionResult Delete(int id)
        {
            var w = db.WorkLogs.FirstOrDefault(x => x.WorkLogId == id);
            if (w == null) return NotFound();
            return View(w);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var w = await db.WorkLogs.FirstOrDefaultAsync(x => x.WorkLogId == id);
                if (w == null) return NotFound();
                db.WorkLogs.Remove(w);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Workers");
            }
            catch
            {
                return View();
            }
        }
    }
}

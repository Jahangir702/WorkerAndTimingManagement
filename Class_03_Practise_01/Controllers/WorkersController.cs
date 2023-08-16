using Class_03_Practise_01.Models;
using Class_03_Practise_01.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Class_03_Practise_01.Controllers
{
    public class WorkersController : Controller
    {
        readonly WorkerDbContext db;
        readonly IWebHostEnvironment env;
        public WorkersController(WorkerDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        // GET: WorkersController
        public async Task<IActionResult> Index(int pg = 1)
        {
            var data = await db.Workers.Include(x => x.WorkLogs).OrderBy(x => x.WorkerId).ToPagedListAsync(pg, 5);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkerInputModel model)
        {

            if (ModelState.IsValid)
            {
                Worker w = new Worker
                {
                    WorkerName = model.WorkerName,
                    PayRate = model.PayRate,
                    Phone = model.Phone,
                    Address = model.Address
                };
                try
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fn = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string sp = Path.Combine(env.WebRootPath, "Pictures", fn);
                    FileStream fs = new FileStream(sp, FileMode.Create);
                    await model.Picture.CopyToAsync(fs);
                    fs.Close();
                    w.Picture = fn;
                    await db.Workers.AddAsync(w);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message, ex);
                }
            }
            return View(model);



        }

        public async Task<ActionResult> Edit(int id)
        {
            var w = await db.Workers.FirstOrDefaultAsync(x => x.WorkerId == id);
            if (w == null) return NotFound();
            ViewBag.Current = w.Picture;
            return View(w);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkerEditModel model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var w = db.Workers.FirstOrDefault(x => x.WorkerId == id);
            if (w == null) return NotFound();
            ViewBag.Current = w.Picture;
            return View(w);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var w = await db.Workers.FirstOrDefaultAsync(x => x.WorkerId == id);
                if (w == null) return NotFound();
                db.Workers.Remove(w);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult WorkerDetails(int id)
        {
            return PartialView("_WorkerDetails", db.Workers.FirstOrDefault(w => w.WorkerId == id));
        }
    }
}

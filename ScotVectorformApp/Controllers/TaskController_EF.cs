using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScotVectorformApp.Data;

namespace ScotVectorformApp.Controllers
{
    public class TaskController_EF : Controller
    {
        private readonly TaskContext _context;

        public TaskController_EF(TaskContext context)
        {
            _context = context;
        }

        // GET: TaskController_EF
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Tasks.ToListAsync());
        //}

        public async Task<IActionResult> Index(string id = "")
        {
            var count = (await _context.Tasks.Where(t => t.Status == "Active").ToListAsync()).Count().ToString();

            ViewData["ActiveCount"] = count;

            if (id == "")
            {
                return View(await _context.Tasks.OrderBy(t => t.Status).ToListAsync());
            }
            else
            {
                var sortedTasks = _context.Tasks.Where(t => t.Status == id);
                return View(await sortedTasks.ToListAsync());
            }
            //foreach (var completedTask in completedTasks)
            //{
            //    var task = await _context.Tasks.FindAsync(completedTask.TaskId);
            //    _context.Tasks.Remove(task);
            //}

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));

        }

        // GET: TaskController_EF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: TaskController_EF/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskController_EF/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,Name,Status")] ScotVectorformApp.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: TaskController_EF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: TaskController_EF/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Name,Status")] ScotVectorformApp.Models.Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: TaskController_EF/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: TaskController_EF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCompleted()
        {
            var completedTasks = _context.Tasks.Where(t => t.Status == "Complete");

            foreach (var completedTask in completedTasks)
            {
                var task = await _context.Tasks.FindAsync(completedTask.TaskId);
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}

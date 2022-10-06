using eTicketing.Data;
using eTicketing.Data.Services;
using eTicketing.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService  _Service;

        public ActorController(IActorService service)
        {
            _Service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _Service.GetAllAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("ActorName,ProfilePictureUrl,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
             _Service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var actordetail = await _Service.GetByIdAsync(id);
            if(actordetail == null)
            {
                return View("Not Found");
            }
            return View(actordetail);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var actordetail = await _Service.GetByIdAsync(id);
            if (actordetail == null)
            {
                return View("NotFound");
            }
            return View(actordetail);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _Service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var actordetail = await _Service.GetByIdAsync(id);
            if (actordetail == null)
            {
                return View("NotFound");
            }
            return View(actordetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteData(int id )
        {
            var actordetail = await _Service.GetByIdAsync(id);
            if (actordetail == null)
            {
                return View("NotFound");
            }
            await _Service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
 
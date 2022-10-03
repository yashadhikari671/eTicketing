using eTicketing.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eTicketing.Controllers
{
    public class ActorController : Controller
    {
        private readonly AppDbcontext _contxet;

        public ActorController(AppDbcontext contxet)
        {
            _contxet = contxet;
        }

        public IActionResult Index()
        {
            var data = _contxet.Actors.ToList();
            return View(data);
        }
    }
}

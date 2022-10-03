using eTicketing.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbcontext _contxet;

        public MovieController(AppDbcontext contxet)
        {
            _contxet = contxet;
        }
        public async Task<IActionResult> Index()
        {
            var allMovie = await _contxet.Movies.Include(n=> n.Cinema).ToListAsync();
            return View(allMovie);
        }
    }
}

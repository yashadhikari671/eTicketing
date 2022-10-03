using eTicketing.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class CinemaController : Controller 
    {
        private readonly AppDbcontext _contxet;

        public CinemaController(AppDbcontext contxet)
        {
            _contxet = contxet;
        }
        public async Task<IActionResult> Index()
        {
            var allcinema = await _contxet.Cinemas.ToListAsync();
            return View(allcinema);
        }
    }
}

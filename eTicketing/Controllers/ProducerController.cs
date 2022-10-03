using eTicketing.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbcontext _contxet;

        public ProducerController(AppDbcontext contxet)
        {
            _contxet = contxet;
        }
        public async Task<IActionResult> Index()
        {
            var allproducer = await _contxet.Producers.ToListAsync();
            return View(allproducer);
        }
    }
}

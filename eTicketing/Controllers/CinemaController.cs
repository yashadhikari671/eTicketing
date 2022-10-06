using eTicketing.Data;
using eTicketing.Data.Services;
using eTicketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class CinemaController : Controller 
    {
        private readonly ICinemaService _service;

        public CinemaController(ICinemaService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allcinema = await _service.GetAllAsync();
            return View(allcinema);
        }
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetail = await _service.GetByIdAsync(id);
            if (cinemaDetail == null) return View("Not Found");
            return View(cinemaDetail);
        }
        //get/producer/create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Createcinema([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetail = await _service.GetByIdAsync(id);
            if (cinemaDetail == null) return View("Not found");
            return View(cinemaDetail);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditProducer(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            if (id == cinema.Id)
            {
                await _service.UpdateAsync(id, cinema);
                return RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetail = await _service.GetByIdAsync(id);
            if (cinemaDetail == null) return View("Not found");
            return View(cinemaDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var producerdetail = await _service.GetByIdAsync(id);
            if (producerdetail == null) return View("Not Found");
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}


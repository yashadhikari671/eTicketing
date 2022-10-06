using eTicketing.Data;
using eTicketing.Data.Services;
using eTicketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMoviesService _services;

        public MovieController(IMoviesService  services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            var allMovie = await _services.GetAllAsync(n => n.Cinema);
            return View(allMovie);
        }
        public async Task<IActionResult> Filter(string Searching)
        {
            var allMovie = await _services.GetAllAsync(n => n.Cinema);
            if (!string.IsNullOrEmpty(Searching))
            {
                var filteredResult = allMovie.Where(n=>(n.Name.ToLower()).Contains(Searching.ToLower()) || (n.Description.ToLower()).Contains(Searching.ToLower())).ToList();
                return View("Index",filteredResult);
            }
            return View("Index",allMovie);
        }
        //get/movie/detail
        
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _services.GetMovieByIdAsync(id);
            return View(movieDetail);
        }
        //git/movie/create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _services.GetNewMovieDropdown();

            ViewBag.CId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.PId = new SelectList(movieDropdownsData.Producers, "Id", "ProducerName");
            ViewBag.AId = new SelectList(movieDropdownsData.Actors, "Id", "ActorName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _services.GetNewMovieDropdown();

                ViewBag.CId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.PId = new SelectList(movieDropdownsData.Producers, "Id", "ProducerName");
                ViewBag.AId = new SelectList(movieDropdownsData.Actors, "Id", "ActorName");
                return View(movie);
            }
            await _services.AddNewMovie(movie);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetail = await _services.GetMovieByIdAsync(id);
            if (movieDetail == null) return View("Not Found");

            var response = new NewMovieVM()
            {
                Id = movieDetail.Id,
                Name = movieDetail.Name,
                Description = movieDetail.Description,
                Price = movieDetail.Price,
                StartDate = movieDetail.StartDate,
                EndDate = movieDetail.EndDate,
                ImageUrl = movieDetail.ImageUrl,
                MovieCategory = movieDetail.MovieCategory,
                CinemaId = movieDetail.CinemaId,
                ProducerId = movieDetail.ProducerId,
                AId = movieDetail.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _services.GetNewMovieDropdown();

            ViewBag.CId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.PId = new SelectList(movieDropdownsData.Producers, "Id", "ProducerName");
            ViewBag.AId = new SelectList(movieDropdownsData.Actors, "Id", "ActorName");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,NewMovieVM movie)
        {
            if (id != movie.Id) return View("Not Found");
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _services.GetNewMovieDropdown();

                ViewBag.CId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.PId = new SelectList(movieDropdownsData.Producers, "Id", "ProducerName");
                ViewBag.AId = new SelectList(movieDropdownsData.Actors, "Id", "ActorName");
                return View(movie);
            }
            await _services.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}

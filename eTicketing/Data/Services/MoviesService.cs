using eTicketing.Data.Base;
using eTicketing.Data.ViewModel;
using eTicketing.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbcontext _context;
        public MoviesService(AppDbcontext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovie(NewMovieVM movie)
        {
            var newMovie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageUrl = movie.ImageUrl,
                CinemaId = movie.ProducerId,
                ProducerId = movie.ProducerId,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,

            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movies Actors
            foreach(var actorId in movie.AId)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.ActorS_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetail = await _context.Movies
                 .Include(c => c.Cinema)
                 .Include(p => p.Producer)
                 .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                 .FirstOrDefaultAsync(n => n.Id == id);
            return movieDetail;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdown()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.ActorName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.ProducerName).ToListAsync()

            };

            return response;
        }

        public async Task UpdateMovieAsync (NewMovieVM movie)
        {
             var dbmovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);
            if(dbmovie != null)
            {
                dbmovie.Name = movie.Name;
                dbmovie.Description = movie.Description;
                dbmovie.Price = movie.Price;
                dbmovie.ImageUrl = movie.ImageUrl;
                dbmovie.CinemaId = movie.ProducerId;
                dbmovie.ProducerId = movie.ProducerId;
                dbmovie.StartDate = movie.StartDate;
                dbmovie.EndDate = movie.EndDate;
                dbmovie.MovieCategory = movie.MovieCategory;
                await _context.SaveChangesAsync();
            };
            //remove all actor
            var existingActorsDb = _context.ActorS_Movies.Where(n => n.MovieId == movie.Id).ToList();
            _context.ActorS_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();
           
            

            //Add Movies Actors
            foreach (var actorId in movie.AId)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await _context.ActorS_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();

        }
    }
}

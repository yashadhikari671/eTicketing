using eTicketing.Models;
using eTicketing.Data.Base;
using System.Threading.Tasks;
using eTicketing.Data.ViewModel;

namespace eTicketing.Data.Services
{
    public interface IMoviesService :IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdown();
        Task AddNewMovie(NewMovieVM movie);
        Task UpdateMovieAsync(NewMovieVM movie);

    }
}

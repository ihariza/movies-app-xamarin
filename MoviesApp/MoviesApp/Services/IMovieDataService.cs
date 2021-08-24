using MoviesApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp.Services
{
    public interface IMovieDataService
    {
        Task<DataWrapper<List<Movie>>> GetMovies(int pageIndex);
        Task<DataWrapper<List<Movie>>> SearchMovies(string query, int pageIndex);
    }
}


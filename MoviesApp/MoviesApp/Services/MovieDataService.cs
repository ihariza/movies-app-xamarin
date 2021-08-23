using MoviesApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviesApp.Services
{
    public class MovieDataService : IMovieDataService
    {
        private const string API_KEY = "6fb34bbf06cb15d7198ec7800c9c5d45";
        private const string BASE_URL = "https://api.themoviedb.org/";
        private const string GET_MOVIES_ENDPOINT = "3/movie/popular";

        private readonly HttpClient httpClient = new HttpClient();

        public async Task<List<Movie>> GetMovies(int pageIndex)
        {
            string url = $@"{BASE_URL}{GET_MOVIES_ENDPOINT}?api_key={API_KEY}&page={pageIndex}";
            string content = await httpClient.GetStringAsync(url);

            MoviesReponseDto response = JsonConvert.DeserializeObject<MoviesReponseDto>(content);
            List<Movie> movies = DataMappers.MoviesDtoToMovies(response.results);
            return movies;
        }
    }
}
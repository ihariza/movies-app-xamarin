using MoviesApp.Models;
using Newtonsoft.Json;
using System;
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
        private const string SEARCH_MOVIES_ENDPOINT = "3/search/movie";

        private readonly HttpClient httpClient = new HttpClient();

        public async Task<DataWrapper<List<Movie>>> GetMovies(int pageIndex)
        {
            string url = $@"{BASE_URL}{GET_MOVIES_ENDPOINT}?api_key={API_KEY}&page={pageIndex}";
            return await ExecuteQuery(url);
        }

        public async Task<DataWrapper<List<Movie>>> SearchMovies(string query, int pageIndex)
        {
            string url = $@"{BASE_URL}{SEARCH_MOVIES_ENDPOINT}?api_key={API_KEY}&query={query}&page={pageIndex}";
            return await ExecuteQuery(url);
        }

        private async Task<DataWrapper<List<Movie>>> ExecuteQuery(string url) {
            DataWrapper<List<Movie>> dataWrapper = new DataWrapper<List<Movie>>();
            try
            {
                string content = await httpClient.GetStringAsync(url);
                MoviesReponseDto response = JsonConvert.DeserializeObject<MoviesReponseDto>(content);
                dataWrapper.Result = DataMappers.MoviesDtoToMovies(response.results);
                dataWrapper.Success = true;
            }
            catch (Exception e)
            {
                dataWrapper.MessageError = e.Message;
                dataWrapper.Success = false;
            }
            return dataWrapper;
        }
    }
}
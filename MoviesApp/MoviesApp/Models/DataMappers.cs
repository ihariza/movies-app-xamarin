using System.Collections.Generic;

namespace MoviesApp.Models
{
    public class DataMappers
    {
        private const string IMAGE_BASE_URL = "https://image.tmdb.org/t/p/w500";

        public static List<Movie> MoviesDtoToMovies(List<MovieDto> movieDtoList)
        {
            List<Movie> movies = new List<Movie>();
            movieDtoList.ForEach(movieDto =>
            {
                movies.Add(
                    new Movie(
                        movieDto.id,
                        movieDto.title,
                        string.IsNullOrWhiteSpace(movieDto.poster_path) ? IMAGE_BASE_URL : IMAGE_BASE_URL + movieDto.poster_path,
                        string.IsNullOrWhiteSpace(movieDto.backdrop_path) ? IMAGE_BASE_URL : IMAGE_BASE_URL + movieDto.backdrop_path,
                        string.IsNullOrWhiteSpace(movieDto.overview) ? null : movieDto.overview,
                        string.IsNullOrWhiteSpace(movieDto.release_date) ? null : movieDto.release_date,
                        string.IsNullOrWhiteSpace(movieDto.vote_average) ? null : movieDto.vote_average
                     )
                );
            });
            return movies;
        }
    }
}

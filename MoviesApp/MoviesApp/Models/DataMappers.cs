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
                            movieDto.title,
                            IMAGE_BASE_URL + movieDto.poster_path,
                            IMAGE_BASE_URL + movieDto.backdrop_path,
                            movieDto.overview ?? AppResources.MoviesNoOverview,
                            movieDto.release_date,
                            movieDto.vote_average
                        )
                    );
            });
            return movies;
        }
    }
}

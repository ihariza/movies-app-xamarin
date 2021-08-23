using System.Collections.Generic;

namespace MoviesApp.Models
{
    public class MoviesReponseDto
    {
        public int page;
        public List<MovieDto> results;
        public int totalPages;
        public int totalResults;
        public string statusMessage;
        public int statusCode;
    }
}

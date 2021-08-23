namespace MoviesApp.Models
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(string title, string posterPath, string backdropPath, string overview, string releaseDate, string voteAverage)
        {
            Title = title;
            PosterPath = posterPath;
            BackdropPath = backdropPath;
            Overview = overview;
            ReleaseDate = releaseDate;
            VoteAverage = voteAverage;
        }

        public string Title { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string VoteAverage { get; set; }
    }
}
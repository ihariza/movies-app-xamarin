namespace MoviesApp.Models
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(int id, string title, string posterPath, string backdropPath, string overview, string releaseDate, string voteAverage)
        {
            Id = id;
            Title = title;
            PosterPath = posterPath;
            BackdropPath = backdropPath;
            Overview = overview;
            ReleaseDate = releaseDate;
            VoteAverage = voteAverage;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string VoteAverage { get; set; }
    }
}
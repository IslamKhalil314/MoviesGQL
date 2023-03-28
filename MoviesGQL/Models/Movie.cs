namespace MoviesGQL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

        public virtual ICollection<MovieActors> MovieActors { get; set; } = new List<MovieActors>();
    }
}

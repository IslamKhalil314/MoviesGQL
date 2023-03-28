namespace MoviesGQL.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public virtual ICollection<MovieActors> MovieActors { get; set; } = new List<MovieActors>();
    }
}

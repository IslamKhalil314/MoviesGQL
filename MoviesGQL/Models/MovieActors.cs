namespace MoviesGQL.Models
{
    public class MovieActors
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual Movie Movies { get; set; } = null!;
        public virtual Actor Actors { get; set; } = null!;
    }
}

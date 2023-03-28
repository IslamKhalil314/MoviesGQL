using MoviesGQL.Models;
using HotChocolate;
using MoviesGQL.Data;

namespace MoviesGQL.GQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Movie> GetMovies([ScopedService] AppDbContext context)
        {
            return context.Movies;
        }
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        public IQueryable<Actor> GetActors([ScopedService] AppDbContext context)
        {
            return context.Actors;
        }
    }
}

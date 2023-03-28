using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using MoviesGQL.Data;
using MoviesGQL.Models;

namespace MoviesGQL.GQL.MovieTypes
{
    public class MovieType : ObjectType<Movie>
    {
        protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
        {
            descriptor.Description("Represents Movie's Id,Name,And it's Actors");

            descriptor.Field(f => f.MovieActors).Ignore();

            descriptor.Field(f => f.Id).Description("Represents Movie's Id");
            descriptor.Field(f => f.Name).Description("Represents Movie's Name");
            descriptor.Field(f => f.Description).Description("Represents A short brief about the movie");

            descriptor.Field(f => f.Actors).ResolveWith<Resolvers>(p => p.GetActors(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("Represents All Actors that participated in the movie");
        }

        private class Resolvers
        {
            public ICollection<Actor> GetActors([Parent] Movie movie, [ScopedService] AppDbContext context)
            {
                var actors = context.Movies.Include(p => p.Actors).FirstOrDefault(m => m.Id == movie.Id)?.Actors;
                return actors;
                //context.Movies.Attach(movie);
                //return movie.Actors;
            }
        }
    }




}

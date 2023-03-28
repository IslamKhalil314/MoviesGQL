using Microsoft.EntityFrameworkCore;
using MoviesGQL.Data;
using MoviesGQL.Models;

namespace MoviesGQL.GQL.ActorTypes
{
    public class ActorType : ObjectType<Actor>
    {
        protected override void Configure(IObjectTypeDescriptor<Actor> descriptor)
        {
            descriptor.Description("Represents Actors");

            descriptor.Field(f => f.MovieActors).Ignore();

            descriptor.Field(f => f.Id).Description("Represents Actor's Id");
            descriptor.Field(f => f.Name).Description("Represents Actor's Name");
            descriptor.Field(f => f.Age).Description("Represents Actor's Age");

            descriptor.Field(f => f.Movies).ResolveWith<Resolvers>(p => p.GetActors(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("Represents All Movies that the actor participated");
        }

        private class Resolvers
        {
            public ICollection<Movie> GetActors([Parent] Actor actor, [ScopedService] AppDbContext context)
            {
                var movies = context.Actors.Include(p => p.Movies).FirstOrDefault(m => m.Id == actor.Id)?.Movies;
                return movies;
            }
        }

    }
}

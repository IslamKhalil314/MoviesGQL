using HotChocolate.Subscriptions;
using HotChocolate.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviesGQL.Data;
using MoviesGQL.GQL.ActorTypes;
using MoviesGQL.GQL.MovieTypes;
using MoviesGQL.Models;

namespace MoviesGQL.GQL
{
    public class Mutaion
    {
        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Adds a Movie.")]
        public async Task<AddMoviePayload> AddMovie(AddMovieInput movie, [ScopedService] AppDbContext context,
            [Service] ITopicEventSender sender, CancellationToken cancellationToken)
        {
            if (movie.ActorsIds is not null)
            {
                if (!movie.ActorsIds.All(x => context.Actors.Find(x) != null))
                    throw new GraphQLException("There is an invalid actor id");

            }

            var _movie = new Movie
            {
                Name = movie.Name,
                Description = movie.Description,
                Actors = movie.ActorsIds is not null ? context.Actors.Where(x => movie.ActorsIds.Contains(x.Id)).ToList() : null
            };

            await context.AddAsync(_movie);
            await context.SaveChangesAsync(cancellationToken);
            await sender.SendAsync(nameof(Subscription.OnMovieAdded), _movie, cancellationToken);
            return new AddMoviePayload(_movie);
        }






        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Adds an Actor.")]
        public async Task<AddActorPayLoad> AddActor(AddActorInput actor, [ScopedService] AppDbContext context,
            [Service] ITopicEventSender sender, CancellationToken cancellationToken)
        {
            if (actor.MoviesIds is not null)
            {
                if (!actor.MoviesIds.All(x => context.Movies.Find(x) != null))
                    throw new GraphQLException("There is an invalid movie id");

            }

            var _actor = new Actor
            {
                Name = actor.Name,
                Age = actor.age,
                Movies = actor.MoviesIds is not null ? context.Movies.Where(x => actor.MoviesIds.Contains(x.Id)).ToList() : null
            };

            await context.AddAsync(_actor);
            await context.SaveChangesAsync(cancellationToken);
            await sender.SendAsync(nameof(Subscription.OnActorAdded), _actor, cancellationToken);

            return new AddActorPayLoad(_actor);
        }
    }
}

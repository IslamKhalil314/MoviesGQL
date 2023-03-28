
using MoviesGQL.Models;

namespace MoviesGQL.GQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        [GraphQLDescription("Subscripe To be notified when a movie is added")]
        public Movie OnMovieAdded([EventMessage] Movie movie)
        {
            return movie;
        }
        [Subscribe]
        [Topic]
        [GraphQLDescription("Subscripe To be notified when a movie is added")]
        public Actor OnActorAdded([EventMessage] Actor actor)
        {
            return actor;
        }
    }
}

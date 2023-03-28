namespace MoviesGQL.GQL.MovieTypes
{
    public record AddMovieInput(string Name, string Description, [GraphQLDescription("hi")] List<int>? ActorsIds);

}

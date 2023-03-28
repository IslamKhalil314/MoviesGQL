namespace MoviesGQL.GQL.ActorTypes
{
    public record AddActorInput(string Name, int age, List<int>? MoviesIds);

}

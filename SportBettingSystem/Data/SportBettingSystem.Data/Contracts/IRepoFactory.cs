namespace SportBettingSystem.Data.Contracts
{
    public interface IRepoFactory
    {
        T Get<T>() where T : class;
    }
}

namespace SportBettingSystem.Data.RepoFactory
{
    using Common.Infrastructure;
    using Contracts;

    public class RepoFactory : IRepoFactory
    {
        public T Get<T>()
            where T : class
        {
            return (T)WebApiDependancy.Resolver.GetService(typeof(T));
        }
    }
}

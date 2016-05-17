namespace SportBettingSystem.Api.Models.Base
{
    using Common.Contracts;
    using Data.Contracts;
    using Microsoft.Practices.Unity;

    public class BaseModel : IModel
    {
        [Dependency]
        public IRepoFactory RepoFactory { get; set; }

        public void Init()
        {
        }
    }
}
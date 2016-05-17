namespace SportBettingSystem.Api.Models.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Base;
    using Common.Contracts;
    using Data.Proxies;
    using Data.Repositories;

    public class MatchModel : BaseModel, IModel<int>
    {
        public int TodayMatchesCount { get; set; }

        public ICollection<MatchProxy> TodayMatches { get; set; }

        public void Init(int data)
        {
            base.Init();
        }

        public void GetTodayMatches(int page = 1, int pageSize = 3)
        {
            this.TodayMatches = this.GetMatchesByPage(page, pageSize).ToList();
        }

        public ICollection<MatchProxy> GetUpdatedMatches(int page = 1, int pageSize = 3)
        {
            var oldTime = DateTime.Now.AddMinutes(-1);

            var updatedMatches = this.GetMatchesByPage(page, pageSize)
                .Where(x => x.SavedAt > oldTime)
                .ToList();

            return updatedMatches;
        }

        private IQueryable<MatchProxy> GetMatchesByPage(int page = 1, int pageSize = 3)
        {
            var matches = this.RepoFactory.Get<MatchRepository>()
                .GetTodayActive();

            var count = matches.Count();
            var itemsToSkip = (pageSize * page) - pageSize;
            var display = Math.Min(count - itemsToSkip, pageSize);

            var result = matches.OrderBy(x => x.Id)
                 .Skip(itemsToSkip)
                 .Take(display);

            this.TodayMatchesCount = count;

            return result;
        }
    }
}


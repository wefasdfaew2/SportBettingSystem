namespace SportBettingSystem.Data.Repositories
{
    using System;
    using System.Linq;

    using Contracts;
    using Models;
    using Proxies;

    public class MatchRepository : BaseRepository<Match>
    {
        public MatchRepository(ISportBettingSystemDbContext context)
            : base(context)
        {
        }

        public Match GetByNumber(string number)
        {
            return this.All().FirstOrDefault(x => x.Number == number);
        }

        public IQueryable<MatchProxy> GetTodayActive()
        {
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddHours(24);

            var result = this.All()
                .Where(x => !x.IsDeleted)
                .Where(x => x.Bets.Any(z => z.Odds.Any()))
                .Where(x => x.StartDate >= startDate && x.StartDate <= endDate);

            return this.GetProxy(result);
        }

        private IQueryable<MatchProxy> GetProxy(IQueryable<Match> result)
        {
            return result.Select(m => new MatchProxy
            {
                Id = m.Id,
                Name = m.Name,
                Number = m.Number,
                Sport = m.Sport,
                Event = m.Event,
                StartDate = m.StartDate.ToString(),
                SavedAt = m.ModifiedOn != null ? m.ModifiedOn : m.CreatedOn,
                MatchType = m.MatchType,
                Bets = m.Bets
                    .Where(x => !x.IsDeleted)
                    .Select(b => new BetProxy
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Number = b.Number,
                        IsLive = b.IsLive,
                        Odds = b.Odds
                            .Where(x => !x.IsDeleted)
                            .Select(o => new OddProxy
                            {
                                Id = o.Id,
                                Name = o.Name,
                                Number = o.Number,
                                Value = o.Value,
                                SpecialBetValue = o.SpecialBetValue
                            })
                    })
            });
        }
    }
}

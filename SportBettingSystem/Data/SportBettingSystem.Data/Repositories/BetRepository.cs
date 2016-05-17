namespace SportBettingSystem.Data.Repositories
{
    using System.Linq;

    using Contracts;
    using Models;

    public class BetRepository : BaseRepository<Bet>
    {
        public BetRepository(ISportBettingSystemDbContext context)
            : base(context)
        {
        }

        public Bet GetByNumber(string number)
        {
            return this.All().FirstOrDefault(x => x.Number == number);
        }
    }
}

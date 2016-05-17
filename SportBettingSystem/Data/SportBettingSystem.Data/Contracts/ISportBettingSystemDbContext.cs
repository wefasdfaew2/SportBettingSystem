namespace SportBettingSystem.Data.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface ISportBettingSystemDbContext
    {
        IDbSet<Match> Matches { get; set; }

        IDbSet<Bet> Bets { get; set; }

        IDbSet<Odd> Odds { get; set; }

        IDbSet<T> Set<T>()
            where T : class;

        DbEntityEntry<T> Entry<T>(T entity)
            where T : class;

        int SaveChanges();

        void Dispose();
    }
}

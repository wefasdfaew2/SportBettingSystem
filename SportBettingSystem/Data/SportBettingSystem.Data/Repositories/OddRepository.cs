namespace SportBettingSystem.Data.Repositories
{
    using System.Linq;

    using Contracts;
    using Models;
    
    public class OddRepository : BaseRepository<Odd>
    {
        public OddRepository(ISportBettingSystemDbContext context)
            : base(context)
        {
        }

        public Odd GetByNumber(string number)
        {
            return this.All().FirstOrDefault(x => x.Number == number);
        }
    }
}

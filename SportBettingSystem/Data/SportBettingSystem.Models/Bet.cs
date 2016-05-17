namespace SportBettingSystem.Models
{
    using System.Collections.Generic;

    using Common.Models;

    public class Bet : BaseModel<int>
    {
        private ICollection<Odd> odds;

        public Bet()
        {
            this.odds = new HashSet<Odd>();
        }

        public bool IsLive { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<Odd> Odds
        {
            get { return this.odds; }
            set { this.odds = value; }
        }
    }
}

namespace SportBettingSystem.Models
{
    using System;
    using System.Collections.Generic;

    using Common.Models;

    public class Match : BaseModel<int> 
    {
        private ICollection<Bet> bets;

        public Match()
        {
            this.bets = new HashSet<Bet>();
        }

        public string Sport { get; set; }

        public string Event { get; set; }

        public string MatchType { get; set; }

        public DateTime StartDate { get; set; }


        public virtual ICollection<Bet> Bets
        {
            get { return this.bets; }
            set { this.bets = value; }
        }
    }
}

namespace SportBettingSystem.Data.Proxies
{
    using System;
    using System.Collections.Generic;

    public class MatchProxy : BaseProxy
    {
        public string Sport { get; set; }

        public string Event { get; set; }

        public string StartDate { get; set; }

        public DateTime? SavedAt { get; set; }

        public string MatchType { get; set; }

        public IEnumerable<BetProxy> Bets { get; set; }
    }
}

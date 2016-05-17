namespace SportBettingSystem.Data.Proxies
{
    using System.Collections.Generic;

    public class BetProxy : BaseProxy
    {
        public bool IsLive { get; set; }

        public IEnumerable<OddProxy> Odds { get; set; }
    }
}

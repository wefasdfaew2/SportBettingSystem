namespace SportBettingSystem.Models
{
    using Common.Models;

    public class Odd : BaseModel<int>
    {
        public float Value { get; set; }

        public float? SpecialBetValue { get; set; }

        public int BetId { get; set; }

        public virtual Bet Bet { get; set; }
    }
}

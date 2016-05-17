namespace SportBettingSystem.Common.Contracts
{
    public interface IModel
    {
        void Init();
    }

    public interface IModel<TFilter>
    {
        void Init(TFilter data);
    }
}

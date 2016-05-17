namespace SportBettingSystem.Data.Contracts
{
    using System.Linq;

    using Common.Models;

    public interface IRepository<T>
        where T : BaseModel<int>
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Detach(T entity);
    }
}

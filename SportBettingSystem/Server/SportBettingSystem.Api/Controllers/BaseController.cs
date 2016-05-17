namespace SportBettingSystem.Api.Controllers
{
    using System.Web.Http;

    using Common.Infrastructure;
    using Common.Contracts;

    public abstract class BaseController : ApiController
    {
        protected WebApiUnityResolver DependencyResolver
        {
            get
            {
                return WebApiDependancy.Resolver;
            }
        }

        protected TModel LoadModel<TModel>()
            where TModel : IModel
        {
            var model = this.DependencyResolver.LoadModel<TModel>();
            model.Init();
            return model;
        }

        protected TModel LoadModel<TModel, TData>(TData data)
            where TModel : IModel<TData>
        {
            var model = this.DependencyResolver.LoadModel<TModel, TData>();
            model.Init(data);
            return model;
        }
    }
}

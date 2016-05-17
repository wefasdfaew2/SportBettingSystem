namespace SportBettingSystem.Contracts.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;

    using Microsoft.Practices.Unity;
    using Interfaces;

    public class UnityResolver : IDependencyResolver, IDisposable
    {
        public readonly IUnityContainer unity;

        public UnityResolver(IUnityContainer unity)
        {
            this.unity = unity;
        }

        public TModel LoadModel<TModel>()
            where TModel : IModel
        {
            return this.unity.Resolve<TModel>();
        }

        public TModel LoadModel<TModel, TData>()
            where TModel : IModel<TData>
        {
            return this.unity.Resolve<TModel>();
        }

        public TModel BuildUp<TModel>(TModel obj)
        {
            return this.unity.BuildUp<TModel>(obj);
        }

        public void Dispose()
        {
            this.unity.Registrations.Where(r => r.LifetimeManager is IDisposable)
                .Select(r => r.LifetimeManager).OfType<IDisposable>().ToList().ForEach(m => m.Dispose());
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.unity.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to resolve type: {0}", serviceType.FullName));
                // By definition of IDependencyResolver contract, this should return null if it cannot be found.
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.unity.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // By definition of IDependencyResolver contract, this should return null if it cannot be found.
                return null;
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = this.unity.CreateChildContainer();
            return new UnityResolver(child);
        }
    }
}

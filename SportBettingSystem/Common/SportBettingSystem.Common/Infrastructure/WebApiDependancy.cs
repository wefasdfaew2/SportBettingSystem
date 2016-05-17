namespace SportBettingSystem.Common.Infrastructure
{
    using Microsoft.Practices.Unity;

    public static class WebApiDependancy
    {
        public static WebApiUnityResolver Resolver;

        public static void GetResolver(IUnityContainer container)
        {
            Resolver = new WebApiUnityResolver(container);
        }
    }
}

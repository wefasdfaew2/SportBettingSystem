using Microsoft.Practices.Unity;
using SportBettingSystem.Contracts.Infrastructure;

namespace SportBettingSystem.Contracts
{
    public static class CommonGlobals
    {
        public static UnityResolver Resolver;

        public static void GetResolver(IUnityContainer container)
        {
            Resolver = new UnityResolver(container);
        }
    }
}

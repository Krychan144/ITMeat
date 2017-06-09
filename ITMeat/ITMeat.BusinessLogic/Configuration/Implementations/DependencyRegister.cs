using ITMeat.BusinessLogic.Action.Base;
using ITMeat.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ITMeat.BusinessLogic.Configuration.Implementations
{
    public static class DependencyRegister
    {
        public static class RegisterDependecy
        {
            public static void Register(IServiceCollection services)
            {
                var dbDependencyBuilder = new DependencyBuilder<IRepository>();
                dbDependencyBuilder.Register(services);

                var blDependencyBuilder = new DependencyBuilder<IAction>();
                blDependencyBuilder.Register(services);
            }
        }
    }
}
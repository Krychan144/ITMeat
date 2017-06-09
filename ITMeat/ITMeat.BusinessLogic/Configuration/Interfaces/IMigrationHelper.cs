using ITMeat.BusinessLogic.Action.Base;

namespace ITMeat.BusinessLogic.Configuration.Interfaces
{
    public interface IMigrationHelper : IAction
    {
        void Migrate();
    }
}
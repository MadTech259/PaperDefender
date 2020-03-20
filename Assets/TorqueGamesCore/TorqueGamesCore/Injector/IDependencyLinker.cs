using Core;
using Core.Injector;

namespace TorqueGamesCore.Injector
{
    public interface IDependencyLinker
    {
        ILinkWithClass<T> LinkInterface<T>() where T : IGameService;
    }
}
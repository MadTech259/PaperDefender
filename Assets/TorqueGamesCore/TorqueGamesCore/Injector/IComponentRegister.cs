using Core.Injector;

namespace TorqueGamesCore.Injector
{
    public interface IComponentRegister
    {
        void AddLinks(IDependencyLinker linker);
    }
}
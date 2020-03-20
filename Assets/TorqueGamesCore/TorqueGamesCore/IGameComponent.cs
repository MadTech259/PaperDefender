using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Core
{
    public interface IGameComponent
    {
        long UID { get; }
        Task StartGame(IServicesInjector services);
        bool UtilLife { get; }
        bool Active { get; }
        void Clear();//prepare to be erased
        IGameComponentState SaveState();
        void LoadState([CanBeNull] IGameComponentState state);
    }
}
using System.Threading.Tasks;
using Core.Injector;
using TorqueGamesCore.Injector;
using UnityEngine;

namespace Core
{
    public abstract class GameComponent : MonoBehaviour , IGameComponent
    {
        public long UID { get; } = Guids.Create();

        public virtual void WriteDependencies(IDependencyLinker linker, IServicesInjector externalDependencies) {}
        public virtual void EarlyInitialization(IServicesInjector gamePlayServices) {}
        public virtual Task StartGame(IServicesInjector services) => Task.CompletedTask;
        public virtual bool UtilLife { get; protected set; } = true;
        public virtual bool Active { get; protected set; } = true;
        public virtual void Clear(){}
        public virtual IGameComponentState SaveState() => null;
        public virtual void LoadState(IGameComponentState state){}


        protected virtual void OnDestroy()
        {
            Clear();
        }
    }

}
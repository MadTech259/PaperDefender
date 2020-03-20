using System.Threading.Tasks;
using Core;
using Core.Injector;
using TorqueGamesCore.Injector;
using UnityEngine;

namespace TorqueGamesCore.Character
{
    public class CharacterComponent : MonoBehaviour
    {
        protected IServicesInjector LocalCharacterDependencies { get; private set; }
        protected IServicesInjector GamePlayDependencies { get; private set; }
        
        public void SetupData(IServicesInjector injector, TorqueGamesInjector playerInjector)
        {
            GamePlayDependencies = injector;
            InitializeLocalDependencies(playerInjector);
        }
        
        public long UID { get; } = Guids.Create();

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

        protected virtual void WriteCharacterLocalDependencies(IDependencyLinker linker) {}
        
        private void InitializeLocalDependencies(TorqueGamesInjector injector)
        {
            WriteCharacterLocalDependencies(injector.GetLinker());
            LocalCharacterDependencies = injector;
        }
    }
}
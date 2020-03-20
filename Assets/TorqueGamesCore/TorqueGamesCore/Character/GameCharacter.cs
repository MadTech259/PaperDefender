using Core;

namespace TorqueGamesCore.Character
{
    public abstract class GameCharacter : GameComponent
    {
        public abstract IServicesInjector Services { get; }
        public abstract void SetupData(ICharacterInstanceData data, IServicesInjector gamePlayInjector);
    }
}
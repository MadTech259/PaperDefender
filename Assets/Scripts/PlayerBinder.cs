using Core;
using TorqueGamesCore.Character;
using TorqueGamesCore.Injector;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBinder : GameComponent, IPlayerBinder
    {
        public Transform Transform => transform;


        public override void WriteDependencies(IDependencyLinker linker, IServicesInjector externalDependencies)
        {
            linker.LinkInterface<IPlayerBinder>().WithGivenInstance(this);
        }
    }
}
using Core;
using TorqueGamesCore.Character;
using TorqueGamesCore.Injector;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBinder : GameComponent, IPlayerBinder
    {

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        public Transform Transform => transform;

        public Animator Animator => _animator;

        public Rigidbody Rigidbody => _rigidbody;
        

        public override void WriteDependencies(IDependencyLinker linker, IServicesInjector externalDependencies)
        {
            linker.LinkInterface<IPlayerBinder>().WithGivenInstance(this);
        }
    }
}
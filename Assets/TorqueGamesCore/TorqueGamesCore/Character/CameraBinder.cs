using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Injector;
using TorqueGamesCore.Injector;
using UnityEngine;
using UnityEngine.Animations;

namespace TorqueGamesCore.Character
{
    public class CameraBinder : GameComponent, ICameraBinder
    {
        
        
        private Camera _camera;
        
        public override void WriteDependencies(IDependencyLinker linker, IServicesInjector gamePlayServices)
        {
            _camera = GetComponent<Camera>();
            linker
                .LinkInterface<ICameraBinder>()
                .WithGivenInstance(this);
        }

        
       

        public Camera GetCamera()
        {
            return _camera;
        }

    }

    public interface IPlayerBinder : IGameService
    {
        Transform Transform { get;}
    }

    public interface ICameraBinder : IGameService
    {
        Camera GetCamera();
    }
}
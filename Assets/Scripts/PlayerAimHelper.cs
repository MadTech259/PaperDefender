using System.Collections;
using System.Collections.Generic;
using Core;
using TorqueGamesCore.Character;
using TorqueGamesCore.Injector;
using TorqueGamesCore.Utilities;
using UnityEngine;

public class PlayerAimHelper : GameComponent, IPlayerAimHelper
{
    [SerializeField] private Transform planeGround;
    private IPlayerBinder PlayerBinder { get; set; }

    private Camera Camera { get; set; }

    public override void WriteDependencies(IDependencyLinker linker, IServicesInjector externalDependencies)
    {
        linker.LinkInterface<IPlayerAimHelper>().WithGivenInstance(this);
    }

    public override void EarlyInitialization(IServicesInjector gamePlayServices)
    {
        Camera = gamePlayServices.Get<ICameraBinder>().GetCamera();
        PlayerBinder = gamePlayServices.Get<IPlayerBinder>();

    }
   

    public bool ShootingDirection(out Vector3 direction)
    {
        var mouseRay = Camera.ScreenPointToRay(Input.mousePosition);
        var upPlane = new Plane(Vector3.up, planeGround.position);
        if (!upPlane.Raycast(mouseRay, out var distance))
        {
            direction = default;
            return false;
        }
        var point = mouseRay.GetPoint(distance);
        var rawDirection = point - PlayerBinder.Transform.position;
        rawDirection.y = 0;
        direction =  rawDirection.normalized;
        return true;
    }
    
}

public interface IPlayerAimHelper : IGameService
{
    bool ShootingDirection(out Vector3 direction);
}


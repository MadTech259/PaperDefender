using System.Collections;
using System.Collections.Generic;
using Core;
using TorqueGamesCore.Injector;
using UnityEngine;

public class PlayerAnimatorModel : GameComponent, IPlayerAnimatorModel
{

    [SerializeField]private Rigidbody rb;

    public override void WriteDependencies(IDependencyLinker linker, IServicesInjector externalDependencies)
    {
        linker.LinkInterface<IPlayerAnimatorModel>().WithGivenInstance(this);
    }
    
    
    

    public float SpeedX
    {
        get { return rb.velocity.x; }
    }

    public float SpeedY
    {
        get { return rb.velocity.z; }
    }
}

public interface IPlayerAnimatorModel : IGameService
{
    float SpeedX { get;}
    float SpeedY { get; }

}

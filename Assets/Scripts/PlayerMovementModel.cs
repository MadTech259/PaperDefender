using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using TorqueGamesCore.Injector;
using UnityEngine;

public interface IPlayerMovementModel : IGameService
{
   KeyCode Up { get; }
   KeyCode Down { get; }
   KeyCode Left { get; }
   KeyCode Right { get; }
   float Speed { get; }
   float MaxSpeed { get; }
}



public class PlayerMovementModel : GameComponent, IPlayerMovementModel
{
   [SerializeField]private KeyCode _up;
   [SerializeField]private KeyCode _down;
   [SerializeField]private KeyCode _left;
   [SerializeField]private KeyCode _right;
   [SerializeField] private float _speed;
   [SerializeField] private float _maxSpeed;
   [SerializeField] private Rigidbody _rb;

   public override void WriteDependencies(IDependencyLinker linker, IServicesInjector externalDependencies)
   {
      linker.LinkInterface<IPlayerMovementModel>().WithGivenInstance(this);
   }

   public KeyCode Up => _up;

   public KeyCode Down => _down;

   public KeyCode Left => _left;

   public KeyCode Right => _right;

   public float Speed => _speed;
   public float MaxSpeed => _maxSpeed;


}



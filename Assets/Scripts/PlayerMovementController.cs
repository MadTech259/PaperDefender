using System.Collections;
using System.Collections.Generic;
using Core;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovementController : GameComponent
{
    
    [SerializeField] private Rigidbody rb;
    private IPlayerMovementModel model;
    private Vector2 direction;
    
    private IPlayerAimHelper PlayerAimHelper { get; set; }
    private Vector3 _aimDirection;

    public override void EarlyInitialization(IServicesInjector gamePlayServices)
    {
        PlayerAimHelper = gamePlayServices.Get<IPlayerAimHelper>();
        model = gamePlayServices.Get<IPlayerMovementModel>();
        //rb = gamePlayServices.Get<PlayerBinder>().Rigidbody;
    }

    private void Update()
    {
        direction = Vector2.zero;
        
        if (Input.GetKey(model.Up))
        {
            direction += Vector2.up;
        }
        
        if (Input.GetKey(model.Down))
        {
            direction += Vector2.down;
        }
        
        if (Input.GetKey(model.Left))
        {
            direction += Vector2.left;
        }
        
        if (Input.GetKey(model.Right))
        {
            direction += Vector2.right;
        }

        direction *= model.Speed;

        if (PlayerAimHelper.ShootingDirection(out var dir))
        {
            Debug.DrawRay(transform.position, dir);
            _aimDirection = dir;
        }
        
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(direction.x, 0,direction.y));
        if (rb.velocity.magnitude > model.MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * model.MaxSpeed;
        }
        
        transform.forward = Vector3.Slerp(transform.forward, _aimDirection, 20 * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class PlayerMovementController : GameComponent
{
    [SerializeField] private PlayerMovementModel model;
    private IPlayerAimHelper PlayerAimHelper { get; set; }
    private Vector3 _aimDirection;

    public override void EarlyInitialization(IServicesInjector gamePlayServices)
    {
        PlayerAimHelper = gamePlayServices.Get<IPlayerAimHelper>();
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;
        
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

        model.Rb.AddForce(new Vector3(direction.x, 0,direction.y));
        if (model.Rb.velocity.magnitude > model.MaxSpeed)
        {
            model.Rb.velocity = model.Rb.velocity.normalized * model.MaxSpeed;
        }
    }

    private void FixedUpdate()
    {
        transform.forward = Vector3.Slerp(transform.forward, _aimDirection, 20 * Time.fixedDeltaTime);
    }
}

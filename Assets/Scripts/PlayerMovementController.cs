using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerMovementModel model;

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
        
        //gameObject.transform.Translate(new Vector3(direction.x, 0,direction.y));
        var rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(direction.x, 0,direction.y));
        if (rb.velocity.magnitude > model.MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * model.MaxSpeed;
        }
    }
}

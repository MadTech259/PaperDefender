using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorModel : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;

    public float speedX
    {
        get { return rb.velocity.x; }
    }

    public float speedY
    {
        get { return rb.velocity.z; }
    }
}

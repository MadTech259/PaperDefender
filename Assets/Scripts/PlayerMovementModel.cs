using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovementModel : MonoBehaviour
{
   [SerializeField]private KeyCode _up;
   [SerializeField]private KeyCode _down;
   [SerializeField]private KeyCode _left;
   [SerializeField]private KeyCode _right;
   [SerializeField] private float _speed;
   [SerializeField] private float _maxSpeed;
   [SerializeField] private Rigidbody _rb;

   public KeyCode Up => _up;

   public KeyCode Down => _down;

   public KeyCode Left => _left;

   public KeyCode Right => _right;

   public float Speed => _speed;
   public float MaxSpeed => _maxSpeed;

   public Rigidbody Rb => _rb;
}

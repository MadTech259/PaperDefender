﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAnimatorModel model;
        
  

    // Update is called once per frame
    void Update()
    {
        Debug.Log(model.speedX +"," + model.speedY + "");
        animator.SetFloat("speedX", model.speedX);
        animator.SetFloat("speedY", model.speedY);
    }
}

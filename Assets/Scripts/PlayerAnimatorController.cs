using System.Collections;
using System.Collections.Generic;
using Core;
using TorqueGamesCore.Character;
using UnityEngine;

public class PlayerAnimatorController : GameComponent
{
    [SerializeField] private Animator animator;
    private IPlayerAnimatorModel model;

    public override void EarlyInitialization(IServicesInjector gamePlayServices)
    {
        model = gamePlayServices.Get<IPlayerAnimatorModel>();
    }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(animator.transform.position + Vector3.up, new Vector3(model.SpeedX,0,  model.SpeedY) );
        animator.SetFloat("speedX", model.SpeedX);
        animator.SetFloat("speedY", model.SpeedY);
    }
}

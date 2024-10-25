using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementRB))]
public class PlayerJump : Interactor
{
    private PlayerMovementRB movement;

    [SerializeField] private float jumpVelocity;

    public override void Interact()
    {
        if(movement == null)
        {
            movement = GetComponent<PlayerMovementRB>();
        }

        if (input.jumpPressed && movement.isGrounded)
        {
            movement.SetJump(jumpVelocity);
        }
    }

    public override void OnStart()
    {
        Debug.Log("Jump");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : Interactor
{
    private PlayerMovement movement;

    [SerializeField] private float jumpVelocity;

    public override void Interact()
    {
        if(movement == null)
        {
            movement = GetComponent<PlayerMovement>();
        }

        if (input.jumpPressed && movement.isGrounded)
        {
            movement.SetJump(jumpVelocity);
        }
    }
}

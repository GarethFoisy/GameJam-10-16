using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeFallState : PlayerState
{
    public PlayerFreeFallState(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        player.CCmovement.SetJump(0);
    }

    public override void OnStateLeave()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (player.input.horizontal != 0 || player.input.vertical != 0)
        {
            player.CCmovement.Move();
        }
        else
        {
            player.CCmovement.ApplyGravity();
        }

        if (player.CCmovement.GroundCheck() && player.CCmovement.GetYVelocity() < 0)
        {
            player.ChangeState(PlayerStateMachine.States.Grounded);
        }
    }
}

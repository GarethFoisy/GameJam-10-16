using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enter move state");
    }

    public override void OnStateLeave()
    {
        
    }

    public override void OnStateUpdate()
    {
        player.CCmovement.Move();

        if (player.input.horizontal == 0 || player.input.vertical == 0)
        {
            //player.ChangeState(PlayerStateMachine.States.Grounded);
        }

        if (player.CCmovement.GroundCheck() && player.input.jumpPressed)
        {
            Debug.Log("attempt to jump");
            player.ChangeState(PlayerStateMachine.States.Jump);
        }
    }
}

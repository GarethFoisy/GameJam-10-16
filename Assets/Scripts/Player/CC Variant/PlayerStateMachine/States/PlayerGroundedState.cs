using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Grounded");
    }

    public override void OnStateLeave()
    {

    }

    public override void OnStateUpdate()
    {
        if (player.input.jumpPressed)
        {
            player.ChangeState(PlayerStateMachine.States.Jump);
        }

        player.CCmovement.Move();
    }
}

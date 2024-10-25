using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine player) : base(player)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Jump State");
        player.jump.Jump();
    }

    public override void OnStateLeave()
    {
        player.CCmovement.SlowFall();
    }

    public override void OnStateUpdate()
    {
        player.CCmovement.Move();

        if (player.input.jumpReleased)
        {
            Debug.Log("Jump release registered");
            player.CCmovement.FastFall();
        }

        if (player.CCmovement.GroundCheck() && player.CCmovement.GetYVelocity() < 0 )
        {
            player.ChangeState(PlayerStateMachine.States.Grounded);
        }
    }
}

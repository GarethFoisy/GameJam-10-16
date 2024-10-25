using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerGrappleState : PlayerState
{
    public PlayerGrappleState(PlayerStateMachine player) : base(player)
    {

    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateLeave()
    {
        
    }

    public override void OnStateUpdate()
    {
        if(!player.grapple.IsGrappling && player.grapple.AtDestination)
        {
            player.ChangeState(PlayerStateMachine.States.FreeFall);
        }
    }
}

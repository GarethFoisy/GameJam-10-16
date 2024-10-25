using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : BaseState
{
    protected PlayerStateMachine player;

    public PlayerState(PlayerStateMachine player)
    {
        this.player = player;
    }
}

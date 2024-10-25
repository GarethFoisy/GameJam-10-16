using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    BaseState currentState;
    private Dictionary<States,BaseState> statesDictionary = new();

    [Header("Components")]
    public PlayerMovementCC CCmovement;
    public PlayerJumpCC jump;
    public PlayerInput input;
    public PlayerGrapple grapple;

    private bool isChangingStates;

    //Key used to change between states
    public enum States 
    {
        Grounded,
        Jump,
        FreeFall,
        Grapple
    }

    void Start()
    {
        statesDictionary[States.Grounded] = new PlayerGroundedState(this);
        statesDictionary[States.Jump] = new PlayerJumpState(this);
        statesDictionary[States.FreeFall] = new PlayerFreeFallState(this);
        statesDictionary[States.Grapple] = new PlayerGrappleState(this);

        input = PlayerInput.GetInstance();

        currentState = statesDictionary[States.Grounded];
        currentState.OnStateEnter();
    }

    void Update()
    {
       

        currentState.OnStateUpdate();

        if (grapple.IsGrappling)
        {
            ChangeState(States.Grapple);
        }
    }

    public void ChangeState(States nextState)
    {
        if (isChangingStates) return;

        isChangingStates = true;
        currentState.OnStateLeave();
        currentState = statesDictionary[nextState];
        currentState.OnStateEnter();
        isChangingStates = false;
    }


}

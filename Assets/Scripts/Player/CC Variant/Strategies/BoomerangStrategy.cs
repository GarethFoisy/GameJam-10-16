using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoomerangLock))]
public class BoomerangStrategy : MonoBehaviour, IEquipStrategy
{
    [SerializeField] BoomerangLock boomerangLock;
    [SerializeField] BoomerangMovement movement;
    [SerializeField] Transform boomerang;

    private PlayerInput input;

    private void Start()
    {
        input = PlayerInput.GetInstance();
    }

    public void UseEquipment()
    {
        if (input.shootHeld)
        {
            boomerangLock.LockOn();
        }

        //Cancel boomerang toss/reset lock
        if (input.activatePressed)
        {
            boomerangLock.EmptyTargets();
        }

        if (input.shootReleased && boomerangLock.Targets.Count > 0)
        {
            boomerang.transform.parent = null;
            movement.SetQueue(boomerangLock.Targets);
            movement.GetNextTarget();
            boomerangLock.EmptyTargets();
        }
    }

    public void OnStrategyChange()
    {
        movement.ResetBoomerang();
    }
}

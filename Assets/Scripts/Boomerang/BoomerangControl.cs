using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoomerangLock))]
public class BoomerangControl : MonoBehaviour
{
    [SerializeField] BoomerangLock boomerangLock;
    [SerializeField] BoomerangMovement movement;
    [SerializeField] Transform boomerang;

    private bool isAiming;

    // Update is called once per frame
    void Update()
    {
        //TODO: Add inputs to Player Input
        if (Input.GetMouseButton(1))
            isAiming = true;
        else
            isAiming = false;

        ZoomCamera();

        if (Input.GetMouseButton(0) && isAiming)
        {
            boomerangLock.LockOn();
        }

        if (Input.GetMouseButtonUp(1))
        {
            boomerangLock.EmptyTargets();
        }

        if (Input.GetMouseButtonUp(0) && boomerangLock.Targets.Count > 0)
        {
            boomerang.transform.parent = null;
            movement.SetQueue(boomerangLock.Targets);
            movement.GetNextTarget();
            boomerangLock.EmptyTargets();
        }
    }

    void ZoomCamera()
    {
        if (isAiming)
        {
            if (Camera.main.fieldOfView > 40)
                Camera.main.fieldOfView -= Time.deltaTime * 40;
            else
                Camera.main.fieldOfView = 40;
        }
        else if (Camera.main.fieldOfView < 60)
            Camera.main.fieldOfView += Time.deltaTime * 40;
        else
            Camera.main.fieldOfView = 60;
    }
}

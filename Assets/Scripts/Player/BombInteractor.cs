using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInteractor : Interactor
{   
    [SerializeField] private Transform playerEye;
    [SerializeField] private Transform attachPoint;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float throwVelocity;

    // pick and drop
    private bool isPicked = false;
    private IPickable pickable;

    public override void Interact() {
        Collider[] hitColliders = Physics.OverlapSphere(playerEye.position, checkRadius, pickupLayer);

        foreach(var collider in hitColliders) {
            if(input.activatePressed && !isPicked) {
                pickable = collider.transform.GetComponent<IPickable>();

                if (pickable == null) 
                    return;

                pickable.OnPicked(attachPoint);
                isPicked = true;
                return;
            }
        }

        if (input.activatePressed && isPicked && pickable != null) {
            pickable.OnDropped();
            isPicked = false;
        }

        if (input.throwPressed && isPicked && pickable != null) {
            pickable.OnThrow(throwVelocity);
            isPicked = false;
        }
    }

    public override void OnStart()
    {
        //throw new System.NotImplementedException();
    }
}

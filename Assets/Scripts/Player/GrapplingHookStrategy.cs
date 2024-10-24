using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookStrategy : Interactor
{
    private PlayerMovement playerMovement;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform playerCam;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask grappleable;
    [SerializeField] private LineRenderer lr;

    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float grappleSpeed;
    [SerializeField] private float grappleDelayTime;

    private Vector3 grapplePoint;
    private bool isGrappling;

    // Start is called before the first frame update
    public override void OnStart()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isGrappling)
        {
            lr.SetPosition(0, shootPoint.position);
        }
    }

    public override void Interact()
    {
        if (input.shootPressed)
        {
            Shoot();
            isGrappling = true;
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, grappleable))
        {
            grapplePoint = hit.point;
            StartCoroutine(ExecuteGrapple(player, grapplePoint, grappleSpeed));
        }
        else
        {
            grapplePoint = playerCam.position + playerCam.forward * maxGrappleDistance;
            Invoke(nameof(ReturnGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    public void ReturnGrapple()
    {
        isGrappling = false;
        lr.enabled = false;
    }

    IEnumerator ExecuteGrapple(GameObject player, Vector3 target, float grappleSpeed)
    {
        while (Vector3.Distance(player.transform.position, target) > 0.1f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, grappleSpeed * Time.deltaTime);
            yield return null;
        }
        Invoke(nameof(ReturnGrapple), grappleDelayTime);
    }
}

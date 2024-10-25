using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookStrategy : Interactor
{
    private PlayerMovementRB playerMovement;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private Camera playerCam;
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
        playerCam = Camera.main;
        playerMovement = GetComponent<PlayerMovementRB>();
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
        Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleable))
        {
            Debug.Log(hit.collider.gameObject.layer);
            grapplePoint = hit.point;
            StartCoroutine(ExecuteGrapple(player, grapplePoint, grappleSpeed));
        }
        else
        {
            grapplePoint = playerCam.transform.position + playerCam.transform.forward * maxGrappleDistance;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerGrapple : MonoBehaviour, IEquipStrategy
{
    [SerializeField] GameObject player;
    Camera playerCam;
    public bool IsGrappling { get; private set; }
    public bool AtDestination { get; private set; }
    private PlayerInput input;
    [SerializeField] private LineRenderer lr;   
    [SerializeField] private Transform shootPoint;
    [SerializeField] float maxGrappleDistance;
    [SerializeField] LayerMask grappleable;

    [SerializeField] private float grappleSpeed;
    [SerializeField] private float grappleDelayTime;

    Vector3 grapplePoint;
    public void Start()
    {
        AtDestination = true;
        playerCam = Camera.main;
        input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        lr.SetPosition(0, shootPoint.position);
    }

    private void Update()
    {
        if (input.shootReleased && IsGrappling)
        {
            ReturnGrapple();
        }
    }

    public void UseEquipment()
    {
        if (input.shootPressed && AtDestination)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleable))
        {
            IsGrappling = true;
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
        IsGrappling = false;
        lr.enabled = false;
    }
    IEnumerator ExecuteGrapple(GameObject player, Vector3 target, float grappleSpeed)
    {
        AtDestination = false;
        while (IsGrappling || AtDestination == false)
        {
            if(Vector3.Distance(player.transform.position, target) < 0.5f)
            {
                AtDestination = true;
            }

            player.transform.position = Vector3.MoveTowards(player.transform.position, target, grappleSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnStrategyChange()
    {
        ReturnGrapple();
    }
}

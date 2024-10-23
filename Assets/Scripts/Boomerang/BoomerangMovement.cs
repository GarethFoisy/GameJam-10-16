using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] Rigidbody boomerangRb;
    [SerializeField] Transform boomerangModel;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationsPerSecond;
    [SerializeField] Transform returnPoint;

    [SerializeField] TrailRenderer[] trailRenderers;

    private Quaternion initialRotation;
    private Transform target;
    public Queue<Transform> targetQueue = new Queue<Transform>();
    private bool isReturning;

    // Update is called once per frame
    private void Start()
    {
        foreach (TrailRenderer tr in trailRenderers)
        {
            tr.emitting = false;
        }
        transform.position = returnPoint.position;
        initialRotation = boomerangModel.rotation;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            MoveToTarget();
        }
    }

    public void SetQueue(IEnumerable<Transform> targets)
    {
        foreach (TrailRenderer tr in trailRenderers)
        {
            tr.emitting = true;
        }

        targetQueue = new Queue<Transform>(targets);
        targetQueue.Enqueue(returnPoint);
    }

    public void GetNextTarget()
    {
        if(targetQueue.Count > 0)
        {
            target = targetQueue.Dequeue();
        }
        else
        {
            //Reset boomerang
            foreach (TrailRenderer tr in trailRenderers)
            {
                tr.emitting = false;
            }

            boomerangModel.rotation = initialRotation;
            boomerangRb.velocity = transform.position;
            boomerangRb.transform.position = returnPoint.position;
            isReturning = false;
        }
    }

    void MoveToTarget()
    {
        boomerangRb.velocity += -(boomerangRb.transform.position - target.position).normalized * moveSpeed;
        boomerangModel.RotateAround(boomerangModel.position, boomerangModel.up, rotationsPerSecond * 360 * Time.deltaTime);

        if(Vector3.Distance(boomerangRb.transform.position, target.position) <= 2f)
        {
            Debug.Log("hit");
            target.GetComponent<ITargetable>().OnHit();
            target = null;
            GetNextTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReturning) return;
        if (other.CompareTag("Obstacle"))
        {
            boomerangRb.velocity = -boomerangRb.velocity * 2;
            ReturnBoomerang();
        }
    }

    void ReturnBoomerang()
    {
        isReturning = true;
        target = returnPoint;
        targetQueue.Clear();
    }
}

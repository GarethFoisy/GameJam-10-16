using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : Interactor
{

    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float gravity;

    [Header("Ground Check")]
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;

    private float moveMultiplier = 1;

    private Rigidbody playerRB;


    public bool isGrounded {  get; private set; }

    public override void OnStart()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;
    }

    public override void Interact()
    {
        GroundCheck();
        moveMultiplier = input.sprintHeld ? sprintMultiplier : 1;
        Vector3 movement = Vector3.zero;

        if (input.horizontal != 0 || input.vertical != 0)
        {
            Move();
        }

        playerRB.velocity += new Vector3(movement.x, gravity * Time.deltaTime, movement.z);
    }

    private void Move()
    {
        Vector3 movement = (playerRB.transform.forward * input.vertical + playerRB.transform.right * input.horizontal);
        playerRB.AddForce(movement.normalized * playerMoveSpeed * moveMultiplier, ForceMode.Force);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask);

        if(isGrounded)
        {
            playerRB.drag = groundDrag;
        }
        else
        {
            playerRB.drag = 0;
        }
    }

    public void SetJump(float jumpForce)
    {
        playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovementCC : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] CharacterController characterController;
    [SerializeField] private Transform cam;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxMoveSpeed;
    private float currentMoveSpeed;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMult = 2;
    [SerializeField] private float minGravityMult = 2;
    [SerializeField] private float maxGravityMult = 4;
    [SerializeField] private PlayerInput input;

    private float smoothTime = 0.2f; //How fast the character turns
    private float smoothVel;


    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;

    Vector3 playerVelocity;
    Vector3 playerMoveDirection;

    //public bool isGrounded { get; private set; }

    public void Start()
    {
        cam = Camera.main.transform;
        input = PlayerInput.GetInstance();
        characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (input.horizontal != 0 || input.vertical != 0)
        {
            moveSpeed += Time.deltaTime * maxMoveSpeed * 2;
        }
        else
        {
            moveSpeed -= Time.deltaTime * maxMoveSpeed * 2;
        }

        if(moveSpeed > maxMoveSpeed)
            moveSpeed = maxMoveSpeed;
        else if(moveSpeed < 0)
            moveSpeed = 0;
    }

    public void Move()
    {
        Vector3 direction = new Vector3 (input.horizontal, 0, input.vertical).normalized;

        if (direction.magnitude > 0.05f)
        {
            float dirAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, dirAngle, ref smoothVel, smoothTime);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 movementDirection = Quaternion.Euler(0, dirAngle, 0f) * Vector3.forward;
            playerMoveDirection = movementDirection;
        }

       
        characterController.Move(playerMoveDirection.normalized * moveSpeed * Time.deltaTime);

        ApplyGravity();

        if (playerVelocity.y < 0 && GroundCheck())
        {
            playerVelocity.y = -1f;
        }
    }

    public void ApplyGravity()
    {
        playerVelocity.y += gravity * gravityMult * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Use to move player in given direction
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="force"></param>
    /// <param name="slowdownSpeed"></param>
    public void ApplyForce(Vector3 direction, float force, float slowdownSpeed = 1)
    {
        StartCoroutine(PushPlayer(direction, force, slowdownSpeed));
    }

    IEnumerator PushPlayer(Vector3 direction, float force, float slowdownSpeed = 1)
    {
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime * slowdownSpeed;
            Vector3 pushForce = direction * force * timer;
            characterController.Move(pushForce * Time.deltaTime);
            yield return null;
        }
    }

    public void FastFall()
    {
        gravityMult = maxGravityMult;
    }

    public void SlowFall()
    {
        gravityMult = minGravityMult;
    }

    public bool GroundCheck()
    {
        return Physics.CheckSphere(groundCheckPos.position, groundCheckDistance, groundLayerMask);
    }

    public float GetForwardSpeed()
    {
        return input.vertical * moveSpeed;
    }

    public void SetJump(float force)
    {
        playerVelocity.y = force;

        Vector3 direction = new Vector3(input.horizontal, 0, input.vertical).normalized;
        if (direction.magnitude > 0.05f)
        {
            float dirAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 movementDirection = Quaternion.Euler(0, dirAngle, 0f) * Vector3.forward;
            ApplyForce(movementDirection, moveSpeed / 2);
        }
    }

    public float GetYVelocity()
    {
        return playerVelocity.y;
    }
}

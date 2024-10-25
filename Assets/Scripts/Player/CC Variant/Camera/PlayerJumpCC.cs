using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementCC))]
public class PlayerJumpCC: MonoBehaviour
{
    [Header("Player Jump")]
    [SerializeField] private float jumpVelocity;

    private PlayerMovementCC CCmovement;

    private void Start()
    {
        if (CCmovement == null)
        {
            CCmovement = GetComponent<PlayerMovementCC>();
        }
    }

    public void Jump()
    {
        CCmovement.SetJump(jumpVelocity);
    }
}

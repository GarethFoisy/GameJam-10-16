using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnMovement : MonoBehaviour
{
    private PlayerInput input;

    [SerializeField] private float turnSpeed;

    private float camXRotation;
    private float camYRotation;

    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInput.GetInstance();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }

    private void Turn()
    {
        //Left and Right camera movements
        //camYRotation += Time.deltaTime * input.mouseX * turnSpeed;
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * input.mouseX);
    }
}

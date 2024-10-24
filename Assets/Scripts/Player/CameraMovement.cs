using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CameraMovement : MonoBehaviour
{
    private PlayerInput input;

    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    private float camXRotation;
    private float camYRotation;

    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }

    private void Turn()
    {
        //Up and Down camera movements
        camXRotation += Time.deltaTime * input.mouseY * turnSpeed * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -75f, 75f);

        camYRotation += Time.deltaTime * input.mouseX * turnSpeed;

        transform.localRotation = Quaternion.Euler(camXRotation, camYRotation, transform.localRotation.z);
    }

    //A method to lock and unlock the cursor in runtime depending if they are in game or in menu
    public void CursorLocked(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

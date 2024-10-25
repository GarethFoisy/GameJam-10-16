using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }
    public bool sprintHeld { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool jumpReleased { get; private set; }
    public bool shootPressed { get; private set; }
    public bool shootHeld { get; private set; }
    public bool shootReleased { get; private set; }
    public bool zoomPressed { get; private set; }
    public bool zoomReleased { get; private set; }
    public bool activatePressed {get; private set; }
    public bool throwPressed {get; private set; }
    public bool equip1pressed {get; private set; }
    public bool equip2pressed {get; private set; }
    public bool equip3pressed {get; private set; }


    private bool clear;

    private static PlayerInput instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    public static PlayerInput GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClearInputs();
        ProcessInput();
    }

    private void FixedUpdate()
    {
        clear = true;
    }

    private void ProcessInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        sprintHeld = sprintHeld || Input.GetButton("Sprint");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpReleased = jumpReleased || Input.GetButtonUp("Jump");

        shootPressed = shootPressed || Input.GetButtonDown("Fire1");
        shootHeld = Input.GetButton("Fire1");
        shootReleased = shootReleased || Input.GetButtonUp("Fire1");

        zoomPressed = zoomPressed || Input.GetButtonDown("Fire2");
        zoomReleased = zoomReleased || Input.GetButtonDown("Fire2");

        activatePressed = activatePressed || Input.GetButtonDown("Interact");
        throwPressed = throwPressed || Input.GetButtonDown("Throw");

        equip1pressed = equip1pressed || Input.GetKeyDown(KeyCode.Alpha1);
        equip2pressed = equip2pressed || Input.GetKeyDown(KeyCode.Alpha2);
        equip3pressed = equip3pressed || Input.GetKeyDown(KeyCode.Alpha3);


    }

    private void ClearInputs()
    {
        if(!clear)
            return;

        horizontal = 0;
        vertical = 0;
        mouseX = 0;
        mouseY = 0;

        sprintHeld = false;
        jumpPressed = false;
        jumpReleased = false;

        shootPressed = false;
        shootReleased = false;

        if (Input.GetButton("Fire2"))
            zoomPressed = true;
        else
            zoomPressed = false;

        zoomReleased = false;

        activatePressed = false;
        throwPressed = false;

        equip1pressed = false;
        equip2pressed = false;
        equip3pressed = false;
    }
}

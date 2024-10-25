using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;

    private float camYRotation;
    private float camXRotation;
    [SerializeField] private float height = 1.5f;
    [SerializeField] private float turnSpeed;
    [SerializeField]Transform player;

    private PlayerInput input;


    private void Start()
    {
        input = PlayerInput.GetInstance();
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    private void MoveCamera()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y + height, player.position.z);
        transform.position = Vector3.Slerp(transform.position, playerPos, _cameraSpeed * Time.deltaTime);
    }

    private void RotateCamera()
    {
        camXRotation += Time.deltaTime * input.mouseY * turnSpeed;
        camXRotation = Mathf.Clamp(camXRotation, -75f, 75f);
        
        camYRotation += Time.deltaTime * input.mouseX * turnSpeed;

        transform.localRotation = Quaternion.Euler(-camXRotation, camYRotation, transform.localRotation.z);
    }

    private void FindPlayer()
    {
        try
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        catch
        {
            Debug.Log("Player Not found");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;

    private float camYRotation;
    private float camXRotation;
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
    }

    private void MoveCamera()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        transform.position = Vector3.Slerp(transform.position, playerPos, _cameraSpeed * Time.deltaTime);
    }
/*
    private void RotateCamera()
    {
        camYRotation = turnSpeed * Time.deltaTime * input.mouseX;
        transform.RotateAround(player.transform.position, transform.up, camYRotation);


        camXRotation += Time.deltaTime * input.mouseY * turnSpeed;
        camXRotation = Mathf.Clamp(camXRotation, -75f, 75f);
    }
*/
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

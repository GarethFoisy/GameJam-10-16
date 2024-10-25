using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private PlayerInput input;
    public bool IsAiming { get; private set; }

    [Header("Field of View Settings")]
    [SerializeField] private float zoomFOV;
    [SerializeField] private float FOV;

    // Update is called once per frame

    private void Start() {
        input = PlayerInput.GetInstance();
    }

    void Update() { 
        if (input.zoomPressed)
        {
            IsAiming = true;
        }
        else
        {
            IsAiming= false;
        }

        ZoomCamera();
    }

    void ZoomCamera() {
        if (IsAiming)
        {
            if (Camera.main.fieldOfView > zoomFOV)
                Camera.main.fieldOfView -= Time.deltaTime * zoomFOV;
            else
                Camera.main.fieldOfView = zoomFOV;
        }
        else if (Camera.main.fieldOfView < FOV)
            Camera.main.fieldOfView += Time.deltaTime * FOV;
        else
            Camera.main.fieldOfView = FOV;
    }
}

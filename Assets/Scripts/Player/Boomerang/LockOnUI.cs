using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnUI : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        // code for the player to pick up
        Destroy(gameObject);
    }
}

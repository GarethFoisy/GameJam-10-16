using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{   
    [SerializeField] private Transform playerRespawnPoint;
    [SerializeField] private GameObject player;

    private Health playerHealth;

    void Start() {
        playerHealth = player.GetComponent<Health>();
    }

    public void Respawn() {
        player.SetActive(false);
        player.transform.SetPositionAndRotation(playerRespawnPoint.position, playerRespawnPoint.rotation);  
        player.SetActive(true);
        playerHealth.ResetHealth();
    }
}

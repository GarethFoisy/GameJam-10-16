using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     [SerializeField] private Health playerHealth;

    public static GameManager instance;

    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    void Start() {
        playerHealth.OnDeath += PlayerDeath;
    }

    void PlayerDeath() {
        gameObject.GetComponent<RespawnPlayer>().Respawn();
    }
}

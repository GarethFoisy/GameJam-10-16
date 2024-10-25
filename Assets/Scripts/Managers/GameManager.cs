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
        LockCursor(true);
        playerHealth.OnDeath += PlayerDeath;
    }

    void PlayerDeath() {
        gameObject.GetComponent<RespawnPlayer>().Respawn();
    }

    public void LockCursor(bool locked) {
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

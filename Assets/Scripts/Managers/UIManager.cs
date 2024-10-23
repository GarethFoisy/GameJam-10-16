using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthBar;

    private float _maxHealth;

    void Start() {
        _maxHealth = playerHealth.GetMaxHealth();
    }

    private void OnEnable() {
        playerHealth.OnHealthUpdate += OnHealthUpdate;
        //playerHealth.OnDeath += OnDeath;
        //playerHealth.OnRestart += OnRestart; 
    }

    private void OnDisable() {
        playerHealth.OnHealthUpdate -= OnHealthUpdate;
    }

    public void OnHealthUpdate(float health) {
        healthBar.value = health/_maxHealth;
    }

}

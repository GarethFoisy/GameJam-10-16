using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{   
    [SerializeField] protected float maxHealth;

    public Action<float> OnHealthUpdate;
    public Action OnDeath, OnRestart;

    public bool isDead { get; private set; }
    protected float health;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth() {
        health = maxHealth;
        if(OnHealthUpdate != null)
        {
            OnHealthUpdate(maxHealth);
        }
        isDead = false;
        // OnRestart();
    }

    public virtual void DeductHealth(float value) {
        if(isDead) return;

        health -= value;

        OnHealthUpdate(health);

        if(health <= 0) {
            isDead = true;
            OnDeath();
            health = 0;
        }
        
    }

    public float GetMaxHealth() {
        return maxHealth;
    }
}

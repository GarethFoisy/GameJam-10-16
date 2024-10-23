using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{   
    public override void DeductHealth(float value) {
        if(isDead) return;

        health -= value;

        OnHealthUpdate(health);
        
        if(health <= 0) {
            health = 0;
            Destroy(gameObject);
        }
    }
}

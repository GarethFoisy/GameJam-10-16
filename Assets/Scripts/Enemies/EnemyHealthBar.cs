using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{   
    [SerializeField] private Health enemyHealth;
    [SerializeField] private Slider healthBar;

    private float _maxHealth;
    Camera mainCamera;

    void Start() {
        _maxHealth = enemyHealth.GetMaxHealth();
        mainCamera = Camera.main;
    }

    void Update(){
        AlignCamera();
    }

    private void OnEnable() {
        enemyHealth.OnHealthUpdate += OnHealthUpdate;
    }

    private void OnDestroy() {
        enemyHealth.OnHealthUpdate -= OnHealthUpdate;
    }

    public void OnHealthUpdate(float health) {
        healthBar.value = health/_maxHealth;
    }

    private void AlignCamera() {
        if (mainCamera != null) {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }
}

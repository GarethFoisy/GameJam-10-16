using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPickable
{   
    [SerializeField] private float damage;
    [SerializeField] private float explosionTime = 3f;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForcePower = 10.0f;
    [SerializeField] private GameObject explosion;

    [SerializeField] private Animator bombAnimator;

    private Rigidbody bombRB;
    private bool canExplode = false;

    // Start is called before the first frame update
    void Start()
    {
        bombRB = GetComponent<Rigidbody>();
        explosion.SetActive(false);
        //_parent = transform.parent;
    }
    
    public void OnPicked(Transform attachTransform) {
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        bombRB.isKinematic = true;
        bombRB.useGravity = false;

        canExplode = true;
    }

    public void OnDropped() {
        bombRB.isKinematic = false;
        bombRB.useGravity = true;
        transform.SetParent(null);

    }

    public void OnThrow(float throwVelocity) {
        bombRB.isKinematic = false;
        bombRB.useGravity = true;
        
        bombRB.velocity = transform.parent.forward * throwVelocity;
        transform.SetParent(null);
    }

    void OnCollisionEnter(Collision other) {
        if(canExplode && other.gameObject.CompareTag("Ground")) {
            bombRB.isKinematic = true;
            StartCoroutine(Explode(explosionTime));
        }
    }

    IEnumerator Explode(float waitTime) {
        bombAnimator.SetTrigger("Explode");
        
        yield return new WaitForSeconds(waitTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(var collider in hitColliders) {
            if(collider.CompareTag("Player") || collider.CompareTag("Enemy")) {
                collider.gameObject.GetComponent<Health>().DeductHealth(damage);

                if (collider.CompareTag("Player")) {
                    PlayerMovementCC player = collider.gameObject.GetComponent<PlayerMovementCC>();
                    player.ApplyForce(collider.transform.position - transform.position, explosionForcePower);
                }
                else {
                    Rigidbody enemy = collider.gameObject.GetComponent<Rigidbody>();
                    enemy.AddForce((collider.transform.position - transform.position)*explosionForcePower/1.5f, ForceMode.Impulse);
                }

                Debug.Log("Bomb explosion damaged "+ collider.gameObject.name);
            }
            if(collider.CompareTag("Destroyable")) {
                Destroy(collider.gameObject);
            }
        }

        Destroy(gameObject);
    }

}

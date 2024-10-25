using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoomerangLock : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxLockRange;
    [SerializeField] private int maxTargets;


    [Header("UI")]
    [SerializeField] private Transform lockOnUIPrefab;
    [SerializeField] private Transform lockOnUICanvas;
    private Queue<Transform> lockIndicators = new();

    public List<Transform> Targets { get; private set; } = new();
    private Camera cam;


    private void Start()
    {
        cam = Camera.main;

        for (int i = 0; i < maxTargets; i++)
        {
            Transform lockOnUI = Instantiate(lockOnUIPrefab, lockOnUICanvas);
            lockIndicators.Enqueue(lockOnUI);
            lockOnUI.gameObject.SetActive(false);
        }
    }

    public void LockOn()
    {
        //Cast Ray
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Spot Lockon layer
        if (Physics.Raycast(ray, out RaycastHit hit, maxLockRange))
        {
            Transform target = hit.transform;

            if (target == null || Targets.Contains(target)) return;

            ITargetable targetInterface = target.GetComponent<ITargetable>();

            if (targetInterface != null)
            {
                targetInterface.OnLockedOn();
                Targets.Add(target);
                if (Targets.Count > maxTargets)
                {
                    Targets.RemoveAt(0);

                    //Sets Lock On UI
                    Transform indicator = lockIndicators.Dequeue();

                    indicator.position = new Vector3(target.position.x,
                        hit.collider.bounds.max.y,
                        target.position.z)
                        + transform.up;

                    lockIndicators.Enqueue(indicator);
                }
                else
                {
                    Transform indicator = lockIndicators.Dequeue();

                    indicator.gameObject.SetActive(true);
                    indicator.position = new Vector3(target.position.x,
                        hit.collider.bounds.max.y,
                        target.position.z) 
                        + transform.up;

                    lockIndicators.Enqueue(indicator);
                }
            }
        }
    }

    public void EmptyTargets()
    {
        Targets.Clear();
        foreach (Transform lockOnUI in lockIndicators)
        {
            lockOnUI.gameObject.SetActive(false);
        }
    }
}

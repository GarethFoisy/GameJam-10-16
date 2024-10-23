using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangReturnPoint : MonoBehaviour, ITargetable
{
    [SerializeField] Transform boomerang;

    public void OnHit()
    {
        Debug.Log("Boomerang Returned");
        boomerang.parent = transform.root;
    }

    public void OnLockedOn()
    {
        Debug.LogWarning("Remove -" + gameObject.name + "-from BoomerangTarget layer.");
    }
}

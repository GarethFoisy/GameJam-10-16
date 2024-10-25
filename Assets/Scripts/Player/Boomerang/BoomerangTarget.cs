using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoomerangTarget : MonoBehaviour, ITargetable
{ 
    [SerializeField] private Material[] materials;
    private MeshRenderer mrenderer;
    private int i;
    private void Start()
    {
        mrenderer = GetComponent<MeshRenderer>();
    }

    public void OnHit()
    {
        i++;
        if(i % 2 == 0)
        {
            mrenderer.material = materials[1];
        }
        else
        {
            mrenderer.material = materials[0];
        }
        Debug.Log(gameObject.name + " hit by boomerang");
    }

    public void OnLockedOn()
    {
        Debug.Log(gameObject.name + " targeted by boomerang");
    }
}

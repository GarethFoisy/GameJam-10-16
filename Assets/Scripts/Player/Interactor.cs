using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    protected PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInput.GetInstance();
        OnStart();
    }

    void Update()
    {
        Interact();   
    }

    public abstract void Interact();

    public abstract void OnStart();
}

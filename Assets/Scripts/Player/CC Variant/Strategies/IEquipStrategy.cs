using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipStrategy 
{
    public abstract void UseEquipment();
    public abstract void OnStrategyChange();
}

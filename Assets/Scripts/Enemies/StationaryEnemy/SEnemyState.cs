using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SEnemyState : BaseState
{
    protected StationaryEnemy enemy;

    public SEnemyState(StationaryEnemy enemy)
    {
        this.enemy = enemy;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : BaseState
{
    protected Enemy enemy;

    public EnemyState(Enemy _enemy)
    {
        this.enemy = _enemy;
    }
}

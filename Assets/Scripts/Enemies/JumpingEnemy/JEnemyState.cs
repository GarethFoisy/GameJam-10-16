using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JEnemyState : BaseState
{
    protected JumpingEnemy enemy;
    public JEnemyState(JumpingEnemy enemy)
    {
        this.enemy = enemy;
    }
}

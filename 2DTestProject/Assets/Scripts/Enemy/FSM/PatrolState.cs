using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.enemyAnimState = 0;
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            enemy.enemyAnimState = 1;
            enemy.MoveToTarget();
        }

        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)
        {
            enemy.TransitionToState(enemy.patrolState);
        }

        if (enemy.attackList.Count > 0)
            enemy.TransitionToState(enemy.attackState);
    }


}

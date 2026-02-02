using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    Enemy enemy;

    public EnemyStateType stateType => EnemyStateType.Attack;

    float attackTimer;
    float attackInterval = 1.2f; // 攻撃間隔（秒）

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        attackTimer = 0f;
        enemy.PlayRandomAttack();
    }

    public void Update()
    {
        if (enemy.isAttacking)
            return;

        float dist = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);

        // 攻撃できない状況なら抜ける
        if (!isView && enemy.isAttacking)
        {
            enemy.ChangeState(enemy.e_statePatrol);
            return;
        }

        if (dist > enemy.attackRange)
        {
            enemy.ChangeState(enemy.e_stateChace);
            return;
        }

        // タイマー更新
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            enemy.PlayRandomAttack();
            attackTimer = 0f;
        }
    }

    public void Exit()
    {
        enemy.isAttacking = false;
    }
}

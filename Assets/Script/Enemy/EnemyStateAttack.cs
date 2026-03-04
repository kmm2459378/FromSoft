using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    Enemy enemy;

    public EnemyStateType stateType => EnemyStateType.Attack;
   


    public void Enter(Enemy enemy)
    {
        Debug.Log("攻撃");
        this.enemy = enemy;
        enemy.attackTimer = 0f;
        enemy.isAttacking = true;

        enemy.currentAttack = enemy.ChooceAttack(enemy.dist);
        enemy.animator.CrossFade(enemy.currentAttack.AttackName, 0.1f);

    }

    public void Update()
    {
        enemy.attackTimer += Time.deltaTime;

        if (enemy.isAttacking && enemy.attackTimer >= enemy.currentAttack.AnimationLength)
        {
            enemy.isAttacking = false;

            // Idleに遷移
            enemy.ChangeState(enemy.e_stateIdle);
        }

        float dist = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);
        // 攻撃できない状況なら抜ける
        if (!isView)
        {
            enemy.ChangeState(enemy.e_statePatrol);
            return;
        }

        if (enemy.dist > enemy.attackRange)
        {
            enemy.ChangeState(enemy.e_stateChace);
            return;
        }

    }


    public void Exit()
    {
        enemy.attackCooldown = 0f;
        enemy.attackInterval = enemy.currentAttack.interval;
        enemy.stateLockUntil = Time.time + 0.5f;
    }


}

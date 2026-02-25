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
        enemy.isAttacking = false;
    }

    public void Update()
    {
       
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

        // 攻撃中なら何もしない
        if (enemy.isAttacking)
            return;

        // タイマー更新
        attackTimer += Time.deltaTime;

        EnemyAttackData attack = enemy.ChooceAttack(enemy.dist);

        if (attack != null && attackTimer >= attack.interval)
        {
            enemy.PlayAttack(attack);
            attackTimer = 0f;
        }
    }


    public void Exit()
    {
        enemy.isAttacking = false;
    }


}

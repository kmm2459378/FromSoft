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
        if (enemy.isAttacking)
            return;
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

        EnemyAttackData attack = enemy.ChooceAttack(enemy.dist);

        //animator.CrossFade(attack.animationStateName, 0.1f);
        enemy.isAttacking = true;
        
    }


    public void Exit()
    {
        enemy.isAttacking = false;
    }


}

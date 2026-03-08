using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    Enemy enemy;
    float patrolTimer = 0f;
    float patrolStart = 5f; // 5秒パトロールしたらIdle

    public EnemyStateType stateType => EnemyStateType.Idle;
    

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.animator.CrossFade("Idle", 0.15f);
        Debug.Log("待機");
    }

    public void Update()
    {
        float dist = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);
        if (!enemy.Attacking) 
        {
            patrolTimer += Time.deltaTime;

            if (isView && dist < enemy.chaseRange)
            {
                enemy.ChangeState(enemy.e_stateChace);
                return;
            }

            if (patrolTimer > patrolStart)
            {
                enemy.ChangeState(enemy.e_statePatrol);
                patrolTimer = 0f;
                return;
            }

        }

        enemy.attackCoolDown += Time.deltaTime;
       
        if (enemy.Attacking && enemy.attackCoolDown > enemy.attackInterval)
        {
            if (isView && enemy.dist <= enemy.attackRange)
            {
                enemy.ChangeState(enemy.e_stateAttack);
            }
        }
    }
    

    public void Exit() 
    {
            
    }
}

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
        patrolTimer += Time.deltaTime;
        float dist = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);

        if (Time.time < enemy.stateLockUntil)
            return;

        if (isView && enemy.dist <= enemy.attackRange)
        {
            enemy.ChangeState(enemy.e_stateAttack);
        }

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
    

    public void Exit() 
    {
            
    }
}

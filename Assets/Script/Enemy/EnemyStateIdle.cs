using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    Enemy enemy;
    float patrolTimer = 0f;
    float patrolStart = 5f; // 5秒パトロールしたらIdleへ

    public EnemyStateType stateType => EnemyStateType.Idle;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        if(enemy.prevState == EnemyStateType.Attack)
        enemy.animator.SetBool("IdleAttack", true);
        else
        enemy.animator.SetBool("Idle", true);
        Debug.Log("待機");
    }

    public void Update()
    {


        patrolTimer += Time.deltaTime;
        float dist = enemy.DistanceToPlayer();

        if (dist < enemy.attackRange)
        {
            enemy.ChangeState(enemy.e_stateAttack);
            return;
        }

        if ( dist < enemy.chaseRange)
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
        if (enemy.prevState == EnemyStateType.Attack)
            enemy.animator.SetBool("IdleAttack", false);
        else
            enemy.animator.SetBool("Idle", false);
    }
}

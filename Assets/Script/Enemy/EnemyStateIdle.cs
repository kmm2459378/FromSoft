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
        enemy.animator.SetBool("Chace", false);
        enemy.animator.SetBool("Idle", true);
        Debug.Log("待機");
    }

    public void Update()
    {


        patrolTimer += Time.deltaTime;
        float dist = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);

        if (isView && dist < enemy.attackRange)
        {
            enemy.ChangeState(enemy.e_stateAttack);
            return;
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
            enemy.animator.SetBool("Idle", false);
    }
}

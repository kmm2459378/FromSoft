using UnityEngine;

public class EnemyStatePatrol : EnemyState
{
    public EnemyStateType stateType => EnemyStateType.Patrol;

    private Enemy enemy;
    private float moveDelay = 1.5f; // 移動を開始するまでの遅延
    private float timer = 0f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.SetRandomDirection();
        enemy.animator.CrossFade("Walk", 0.7f);
        timer = 0f; 
        enemy.patrolTimer = 0f;
        enemy.moveSpeed = enemy.Speed;

        Debug.Log("パトロール");
    }

    public void Update()
    {
        float dist = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);

        enemy.patrolTimer += Time.deltaTime;
        timer += Time.deltaTime;

        if (enemy.patrolTimer > enemy.maxPatrolTime)
        {
            enemy.ChangeState(enemy.e_stateIdle);
            return;
        }

        if (isView && dist < enemy.chaseRange)
        {
            enemy.ChangeState(enemy.e_stateChace);
            return;
        }


        if (timer >= moveDelay)
        {
            enemy.RequestMove(enemy.moveDir);

            if (Random.value < enemy.directionChangeRate)
            {
                enemy.SetRandomDirection();
            }
        }
    }

    public void Exit()
    {
        enemy.moveSpeed = enemy.Speed;
    }
}


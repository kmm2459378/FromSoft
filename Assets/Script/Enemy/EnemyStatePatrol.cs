using UnityEngine;

public class EnemyStatePatrol : EnemyState
{
    public EnemyStateType stateType => EnemyStateType.Patrol;

    private Enemy enemy;
    private float moveDelay = 1f; // 移動を開始するまでの遅延
    private float timer = 0f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.SetRandomDirection();
        enemy.animator.SetBool("Walk", true);
        timer = 0f; 
        enemy.patrolTimer = 0f;
        enemy.moveSpeed = enemy.patrolMoveSpeed;

        Debug.Log("パトロール");
    }

    public void Update()
    {
        enemy.patrolTimer += Time.deltaTime;
        timer += Time.deltaTime;

        if (enemy.patrolTimer > enemy.maxPatrolTime)
        {
            enemy.ChangeState(enemy.e_stateIdle);
            return;
        }

        float dist = enemy.DistanceToPlayer();
        if (dist < enemy.chaseRange)
        {
            enemy.ChangeState(enemy.e_stateChace);
            return;
        }

        //アニメーションが始まるまで移動禁止
        if (timer < moveDelay) return;
        enemy.RequestMove(enemy.moveDir);

        if (Random.value < enemy.directionChangeRate)
        {
            enemy.SetRandomDirection();
        }
    }

    public void Exit()
    {
        enemy.animator.SetBool("Walk", false);
        enemy.moveSpeed = enemy.normalSpeed;
    }
}


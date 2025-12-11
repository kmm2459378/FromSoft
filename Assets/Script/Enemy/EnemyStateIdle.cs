using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    Enemy enemy;
    float patrolTimer = 0f;
    float patrolStart = 10f; // 5秒パトロールしたらIdleへ

    public EnemyStateType stateType => EnemyStateType.Idle;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("Idle", true);
        Debug.Log("待機");
    }

    public void Update()
    {
        patrolTimer += Time.deltaTime;
        float dist = Vector3.Distance(enemy.transform.position, enemy.Player.transform.position);
        if(dist < 6f)
        {
            enemy.ChangeState(enemy.e_stateChace);
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

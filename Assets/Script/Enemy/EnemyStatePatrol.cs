using UnityEngine;

public class EnemyStatePatrol : EnemyState
{
    Enemy enemy;
    public EnemyStateType stateType => EnemyStateType.Patrol;
    Vector3 moveDir;
    float patrolTime = 0;
    float maxPatrolTime = 5f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("パトロール");

        moveDir = Random.insideUnitSphere;
        moveDir.y = 0;           
        moveDir.Normalize();
    }

    public void Update()
    {
        patrolTime += Time.deltaTime;
        if(patrolTime > maxPatrolTime)
        {
            enemy.ChangeState(enemy.e_stateIdle);
            patrolTime = 0f;
            return;
        }

        float dist = Vector3.Distance(enemy.transform.position, enemy.Player.position);
        if (dist < 8f)
        {
            enemy.ChangeState(enemy.e_stateChace);
            return;
        }

        // ▼滑らか回転
        Quaternion targetRot = Quaternion.LookRotation(moveDir);
        enemy.transform.rotation = Quaternion.Slerp(
            enemy.transform.rotation,
            targetRot,
            3f * Time.deltaTime
        );

        // ▼滑らか移動
        enemy.transform.position += enemy.transform.forward * 1f * Time.deltaTime;

        // ランダムに方向変更
        if (Random.value < 0.002f)
        {
            moveDir = Random.insideUnitSphere;
            moveDir.y = 0;
        }

    }

    public void Exit()
    {
      Vector3 toPlayer = enemy.Player.position - enemy.transform.position;
      toPlayer.y = 0f;
      
      enemy.transform.rotation = Quaternion.LookRotation(toPlayer);
    }
}


using UnityEngine;

public class EnemyStatePatrol : EnemyState
{
    Enemy enemy;
    float direction = 1;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("パトロール");
    }

    public void Update()
    {
        enemy.transform.Translate(Vector3.right * direction * 1f * Time.deltaTime);

        // 適当に反転（デモ用）
        if (Random.value < 0.001f)
            direction *= -1;

        float dist = Vector3.Distance(enemy.transform.position, enemy.Player.position);

        if (dist < 6f)
            enemy.ChangeState(enemy.e_stateChace);
    }

    public void Exit() { }
}


using UnityEngine;

public class EnemyStateChace : EnemyState
{
    Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("í«ê’");
    }

    public void Update()
    {
        Vector3 dir = (enemy.Player.position - enemy.transform.position).normalized;
        enemy.transform.position += dir * 2f * Time.deltaTime;

        float dist = Vector3.Distance(enemy.transform.position, enemy.Player.position);

        if (dist < 1.5f)
            enemy.ChangeState(enemy.e_stateAttack);

        if (dist > 8f)
            enemy.ChangeState(enemy.e_statePatrol);
    }

    public void Exit() { }
}

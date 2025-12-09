using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    Enemy enemy;
    public EnemyStateType stateType => EnemyStateType.Attack;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("UŒ‚");
    }

    public void Update()
    {
        float dir = Vector3.Distance(enemy.transform.position, enemy.Player.position);

        if (dir < 1.5f)
        {
            enemy.ChangeState(enemy.e_stateAttack);
        }

        else 
        {
            enemy.ChangeState(enemy.e_statePatrol);
        }
    }

    public void Exit()
    {

    }
}

using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("‘Ò‹@");
    }

    public void Update()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.Player.transform.position);
        if(dist < 6f)
        {
            enemy.ChangeState(enemy.e_stateChace);
        }

        if(dist > 8f)
        {
            enemy.ChangeState(enemy.e_statePatrol);
        }
    }

    public void Exit() 
    { 
       
    }
}

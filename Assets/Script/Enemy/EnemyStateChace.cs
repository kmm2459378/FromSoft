using UnityEngine;

public class EnemyStateChace : EnemyState
{
    Enemy enemy;
    public EnemyStateTypeÅ@stateType => EnemyStateType.Chace;
    

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.SetToPlayerDirection();
        enemy.animator.CrossFade("Run", 0.15f);
        enemy.moveSpeed = enemy.chaceMoveSpeed;

        Debug.Log("í«ê’");
    }

    public void Update()
    {
        enemy.SetToPlayerDirection(); 

        float dir = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);

        if (isViewÅ@&& dir < enemy.attackRange)
        {
            enemy.ChangeState(enemy.e_stateAttack);
            return;
        }

        if (!isView && dir < enemy.patrolRange)
        {
            enemy.ChangeState(enemy.e_statePatrol);
            return;
        }


        enemy.RequestMove(enemy.moveDir);

    }



    public void Exit() 
    {
        enemy.moveSpeed = enemy.normalSpeed;
    }
}

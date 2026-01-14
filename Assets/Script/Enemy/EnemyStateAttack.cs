using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    Enemy enemy;
    public EnemyStateType stateType => EnemyStateType.Attack;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("Attack", true);
        Debug.Log("UŒ‚");

    }

    public void Update()
    {
        AnimatorStateInfo info = enemy.animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Attack") && info.normalizedTime >= 1f)
        {
            enemy.ChangeState(enemy.e_stateIdle);  
        }

    }

    public void Exit()
    {
        enemy.animator.SetBool("Attack", false);
    }
}

using UnityEngine;


public class Enemy : MonoBehaviour
{
    EnemyState e_state;
    public Transform Player;

    public EnemyStateIdle   e_stateIdle = new EnemyStateIdle();
    public EnemyStateAttack e_stateAttack = new EnemyStateAttack();
    public EnemyStateJump   e_statejump = new EnemyStateJump();
    public EnemyStateChace  e_stateChace = new EnemyStateChace();
    public EnemyStatePatrol e_statePatrol = new EnemyStatePatrol();

    private void Start()
    {
        ChangeState(e_stateIdle);
    }

     void Update()
    {
        e_state.Update();
    }

    public void ChangeState(EnemyState next)
    {
        if (e_state == next) return;

        e_state?.Exit();
        e_state = next;
        e_state.Enter(this);

    }
}

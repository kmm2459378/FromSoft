using UnityEngine;


public class Enemy : MonoBehaviour
{
    EnemyState e_state;
    EnemyStateType e_type;
    public Animator animator { get; private set; }
    public Transform Player;

    public EnemyStateIdle   e_stateIdle = new EnemyStateIdle();
    public EnemyStateAttack e_stateAttack = new EnemyStateAttack();
    public EnemyStateJump   e_statejump = new EnemyStateJump();
    public EnemyStateChace  e_stateChace = new EnemyStateChace();
    public EnemyStatePatrol e_statePatrol = new EnemyStatePatrol();

    private void Start()
    {
        animator = GetComponent<Animator>();
        ChangeState(e_stateIdle);
    }


    public void ChangeState(EnemyState next)
    {
        if (e_state == next) return;

        e_state?.Exit();
        e_state = next;
        e_type = e_state.stateType;
        e_state.Enter(this);

    }

    void Update()
    {
        e_state?.Update();
    }
}

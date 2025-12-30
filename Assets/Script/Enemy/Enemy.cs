using UnityEngine;


public class Enemy : MonoBehaviour
{
    EnemyState e_state;
    public EnemyStateType e_type;
    public Animator animator { get; private set; }
    public Transform Player;
    public Rigidbody rb { get; private set; }

    public EnemyStateIdle e_stateIdle = new EnemyStateIdle();
    public EnemyStateAttack e_stateAttack = new EnemyStateAttack();
    public EnemyStateChace e_stateChace = new EnemyStateChace();
    public EnemyStatePatrol e_statePatrol = new EnemyStatePatrol();

    // パトロール関連
    public float patrolTimer = 0f;
    public float maxPatrolTime = 10f;
    public float moveSpeed = 1.5f;
    public float rotateSpeed = 3f;
    public float chaseRange = 8f;


    // ランダム方向
    public Vector3 moveDir;
    public float directionChangeRate = 0.002f;

    // 移動指示用バッファ
    private Vector3 requestedDir = Vector3.zero;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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

    void FixedUpdate()
    {
        if (requestedDir != Vector3.zero)
        {
            // 回転
            Quaternion targetRot = Quaternion.LookRotation(requestedDir);
            Quaternion newRot = Quaternion.Slerp(
                rb.rotation,
                targetRot,
                rotateSpeed * Time.fixedDeltaTime
            );
            rb.MoveRotation(newRot);

            // 移動
            Vector3 nextPos = rb.position + rb.transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPos);

            requestedDir = Vector3.zero;
        }
    }

    public void SetRandomDirection()
    {
        moveDir = Random.insideUnitSphere;
        moveDir.y = 0;
        moveDir.Normalize();
    }

    public void RequestMove(Vector3 dir)
    {
        requestedDir = dir; 
    }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, Player.position);
    }
}

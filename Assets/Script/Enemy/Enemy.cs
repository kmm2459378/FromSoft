using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    EnemyState e_state;
    public EnemyStateType e_type;
    public EnemyStateType prevState;

    // 距離ごとの攻撃リスト
    public List<EnemyAttackData> closeAttackList;
    public List<EnemyAttackData> middleAttackList;
    public List<EnemyAttackData> longAttackList;
    public Animator animator { get; private set; }
    public Transform Player;
    public Rigidbody rb { get; private set; }

    public EnemyStateIdle e_stateIdle;
    public EnemyStateAttack e_stateAttack;
    public EnemyStateChace e_stateChace;
    public EnemyStatePatrol e_statePatrol;


    //エネミーのステータス

    [SerializeField] private EnemyType enemyType;
    public int HP { get; set; }
    public float Speed{ get; set; }
    public int AttackDamage{ get; set; }

    public float moveSpeed = 0;
    public int fieldView = 140;
    public float dist { get; set; } = 0;


    // パトロール関連
    public float patrolTimer = 0f;
    public float maxPatrolTime = 10f;
    public float rotateSpeed = 3f;
    public float chaseRange = 8f;

    //追いかけ・アタック関連
    public float attackRange = 20.0f;
    public float patrolRange = 8f;
    public float chaceMoveSpeed = 5f;
   
    // ランダム方向
    public Vector3 moveDir;
    public float directionChangeRate = 0.002f;

    // 移動指示用バッファ
    private Vector3 requestedDir = Vector3.zero;

    //攻撃関連
    public bool Attacking = false;
    public EnemyAttackData currentAttack;
    public float attackCoolDown = 0f;
    public float attackInterval = 0f;
    public float attackTimer;
    public float stateLockUntil = 0f;
    public int comboIndex;


    void Awake()
    {
        if (!EnemyDataBase.enemyStatusDic.ContainsKey(enemyType))
        {
            Debug.LogError("EnemyTypeが登録されていません: " + enemyType);
            return;
        }

        EnemyStatus status = EnemyDataBase.enemyStatusDic[enemyType];

        HP = status.HP;
        Speed = status.Speed;
        AttackDamage = status.AttackDamage;
        e_stateIdle = new EnemyStateIdle();
        e_stateAttack = new EnemyStateAttack();
        e_stateChace = new EnemyStateChace();
        e_statePatrol = new EnemyStatePatrol();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ChangeState(e_stateIdle);
    }


    public void ChangeState(EnemyState next)
    {
        if (e_state == next) return;

        if (e_state != null)
        {
            prevState = e_state.stateType;
            e_state.Exit();
        }
        else
        {
            prevState = next.stateType;
        }
        e_state = next;
        e_type = e_state.stateType;
        e_state.Enter(this);

    }

    void Update()
    {
        dist = DistanceToPlayer();
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

    //向く方向
    public void SetRandomDirection()
    {
        moveDir = Random.insideUnitSphere;
        moveDir.y = 0;
        moveDir.Normalize();
    }

    //プレイヤーから敵の距離
    public void SetToPlayerDirection()
    {
        moveDir = (Player.position - transform.position);
        moveDir.y = 0;
        moveDir.Normalize();
    }

    public void RequestMove(Vector3 dir)
    {
        requestedDir = dir; 
    }

    //プレイヤーとの距離
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, Player.position);
    }

    //視野の中にいるのか
    public bool IsPlayerInView(float viewAngle, float viewDistance)
    {
        Vector3 toPlayer = Player.position - transform.position;
        toPlayer.y = 0f;

        if (toPlayer.magnitude > viewDistance)
            return false;

        float angle = Vector3.Angle(transform.forward, toPlayer.normalized);
        return angle <= viewAngle * 0.5f;
    }

    // //距離で種類を決める
    public AttackType GetAttackRange(float dist)
    {
        if (dist <= 3f)
            return AttackType.Close;

        //if (dist <= 10f)
            return AttackType.Middle;

        //return AttackType.Long;
    }

    //距離の技の中から一つ選ぶ
    public EnemyAttackData ChooseAttack(float dist)
    {
        AttackType type = GetAttackRange(dist);

        List<EnemyAttackData> list = null;

        switch (type)
        {
            case AttackType.Close:
                list = closeAttackList;
                break;

            case AttackType.Middle:
                list = middleAttackList;
                break;

            case AttackType.Long:
                list = longAttackList;
                break;
        }

        if (list == null || list.Count == 0)
            return null;

        return list[Random.Range(0, list.Count)];
    }

    public void ExecuteAttack()
    {
        if (currentAttack == null) return;

        switch (currentAttack.attackType)
        {
            case EnemyAttackType.Dash:

                DashAttack(
                    currentAttack.dashSpeed,
                    currentAttack.dashTime
                );

                break;

            case EnemyAttackType.Jump:

                JumpAttack(
                    currentAttack.jumpForce
                );

                break;

            case EnemyAttackType.Combo:

                break;
        }
    }

    public void DashAttack(float speed, float time)
    {
        StartCoroutine(DashCoroutine(speed, time));
    }

    IEnumerator DashCoroutine(float speed, float time)
    {
        float timer = 0;

        Vector3 dashDir = (Player.position - transform.position);
        dashDir.y = 0;
        dashDir.Normalize();

        transform.forward = dashDir;

        while (timer < time)
        {
            RequestMove(dashDir);
            moveSpeed = speed;

            timer += Time.deltaTime;
            yield return null;
        }

        moveSpeed = 0;
    }

    //ジャンプ攻撃
    public void JumpAttack(float force)
    {
        Vector3 targetPos = Player.position;

        Vector3 dir = targetPos - transform.position;
        dir.y = 0;
        dir.Normalize();

        transform.forward = dir;

        rb.AddForce(
            dir * force + Vector3.up * force,
            ForceMode.Impulse
        );
    }


    //ダメージを食らった場合
    public void TakeDamage(int damage)
    {
        HP -= damage;

        // ヒットモーション
        animator.CrossFade("Damage", 0.1f);

        if (HP <= 0)
        {
            Die();
        }
    }

    //死亡演出
    void Die()
    {
        animator.CrossFade("Die",0.1f );

        // AI停止
        e_state = null;

        // 移動停止
        moveSpeed = 0;
        rb.linearVelocity = Vector3.zero;

        // 3秒後削除
        Destroy(gameObject, 3f);
    }

}

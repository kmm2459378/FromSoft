using UnityEngine;

public class EnemyStateChace : EnemyState
{
    Enemy enemy;
    public EnemyStateTypeÅ@stateType => EnemyStateType.Chace;
    Rigidbody rb;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        rb = enemy.GetComponent<Rigidbody>();
        Debug.Log("í«ê’");
    }

    public void Update()
    {
        Vector3 dir = (enemy.Player.position - enemy.transform.position).normalized;
        dir.y = 0;


        if (dir.magnitude < 1.5f)
            enemy.ChangeState(enemy.e_stateAttack);

        if (dir.magnitude > 8f)
            enemy.ChangeState(enemy.e_statePatrol);
    }

    void FixedUpdate()
    {
        Vector3 dir = (enemy.Player.position - enemy.transform.position).normalized;
        dir.y = 0;
       
        Quaternion targetRot = Quaternion.LookRotation(dir);
        Quaternion newRot = Quaternion.Slerp(
            enemy.transform.rotation,
            targetRot,
            5f * Time.deltaTime
        );
        rb.MoveRotation(newRot);
      
        Vector3 nextPos = enemy.transform.forward * 3f * Time.deltaTime;
        rb.MovePosition(nextPos);
    }

    public void Exit() { }
}

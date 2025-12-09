using UnityEngine;

public class EnemyStateChace : EnemyState
{
    Enemy enemy;
    public EnemyStateType　stateType => EnemyStateType.Chace;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("追跡");
    }

    public void Update()
    {
        Vector3 dir = (enemy.Player.position - enemy.transform.position).normalized;
        dir.y = 0;
        // ▼滑らかにプレイヤーの方向を向く
        Quaternion targetRot = Quaternion.LookRotation(dir);
        enemy.transform.rotation = Quaternion.Slerp(
            enemy.transform.rotation,
            targetRot,
            5f * Time.deltaTime
        );

        // ▼滑らかに追いかける
        enemy.transform.position += enemy.transform.forward * 3f * Time.deltaTime;

        if (dir.magnitude < 1.5f)
            enemy.ChangeState(enemy.e_stateAttack);

        if (dir.magnitude > 8f)
            enemy.ChangeState(enemy.e_statePatrol);
    }

    public void Exit() { }
}

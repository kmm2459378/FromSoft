using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    Enemy enemy;

    public EnemyStateType stateType => EnemyStateType.Attack;
   


    public void Enter(Enemy enemy)
    {
        Debug.Log("攻撃");
        this.enemy = enemy;
        enemy.attackTimer = 0f;
        enemy.Attacking = true;
        this.enemy = enemy;

        float dist = enemy.DistanceToPlayer();

        enemy.currentAttack = enemy.ChooseAttack(dist);

        if (enemy.currentAttack == null)
            return;

        enemy.comboIndex = 0;

        // 攻撃実行
        enemy.ExecuteAttack();

        if (enemy.currentAttack.comboAnimations.Count > 0)
        {
            enemy.animator.CrossFade(
                enemy.currentAttack.comboAnimations[0],
                0.1f
            );
        }
    }

    public void Update()
    {

        float dist = enemy.DistanceToPlayer();


        if (dist > enemy.attackRange)
        {
            Debug.Log("距離: " + dist);
            enemy.ChangeState(enemy.e_stateChace);
            return;
        }

        enemy.attackTimer += Time.deltaTime;

        if (enemy.attackTimer >= enemy.currentAttack.AnimationLength)
        {
            enemy.comboIndex++;
            enemy.attackTimer = 0f;

            if (enemy.comboIndex < enemy.currentAttack.comboAnimations.Count)
            {
                enemy.animator.CrossFade(
                    enemy.currentAttack.comboAnimations[enemy.comboIndex],
                    0.1f
                );
            }
            else
            {
                enemy.ChangeState(enemy.e_stateIdle);
            }


        }
    }

    public void Exit()
    {
        // プレイヤー方向を取得
        Vector3 dir = enemy.Player.position - enemy.transform.position;
        dir.y = 0;
        dir.Normalize();

        // プレイヤーを向く
        enemy.transform.forward = dir;
        enemy.attackCoolDown = 0f;
        enemy.attackInterval = enemy.currentAttack.interval;
        enemy.stateLockUntil = Time.time + 0.5f;
    }


}

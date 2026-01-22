using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    Enemy enemy;
    bool hasPlayedAttack = false;　　//アタックしてたかどうか
    public EnemyStateType stateType => EnemyStateType.Attack;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        hasPlayedAttack = false;
        enemy.animator.SetBool("Attack", true);
        enemy.animator.SetBool("Chace", true);
        Debug.Log("攻撃");

    }

    public void Update()
    {
     
        AnimatorStateInfo info = enemy.animator.GetCurrentAnimatorStateInfo(0);

        // 攻撃アニメに入った瞬間を検知
        if (!hasPlayedAttack && info.IsName("Attack") && info.normalizedTime >= 0.8f)
        {
            hasPlayedAttack = true;
        }

        // まだ一度も攻撃アニメが再生されていないなら何もしない
        if (!hasPlayedAttack)
            return;

        float dist = enemy.DistanceToPlayer();
        bool isView = enemy.IsPlayerInView(enemy.fieldView, enemy.chaseRange);

        // 攻撃距離を離れたら追跡に戻る
        if (isView && dist > enemy.attackRange)
        {
            enemy.ChangeState(enemy.e_stateChace);
            return;
        }

        // 見失ったらpatrolへ
        if (!isView)
        {
            enemy.ChangeState(enemy.e_statePatrol);
            return;
        }
    }
    public void Exit()
    {
        enemy.animator.SetBool("Attack", false);
    }
}

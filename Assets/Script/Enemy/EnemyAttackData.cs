using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Scriptable Objects")]
public class EnemyAttackData : ScriptableObject
{
    [Header("基本情報")]
    public List<string> comboAnimations;  // コンボアニメーション
    public float AnimationLength;

    [Header("攻撃性能")]
    public float damege;

    [Header("クールタイム")]
    public float interval;

    [Header("攻撃の種類")]
    public AttackType AttackType;

    [Header("技の種類")]
    public EnemyAttackType attackType;

    [Header("ダッシュの技")]
    public float dashSpeed;
    public float dashTime;
    [Header("ジャンプの飛距離")]
    public float jumpForce;
}

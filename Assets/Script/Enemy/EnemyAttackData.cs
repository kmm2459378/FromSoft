using UnityEngine;
using UnityEngine.Rendering.UI;

[CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Scriptable Objects")]
public class EnemyAttackData : ScriptableObject
{
    [Header("基本情報")]
    public int ID;
    public string AttackName;
    public float AnimationLength;

    [Header("攻撃性能")]
    public float damege;
    public float range;

    [Header("クールタイム")]
    public float interval;

    [System.NonSerialized]
    public int stateHash;   // 実行時に自動生成
}

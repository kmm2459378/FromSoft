using UnityEngine;

[System.Serializable]
public class EnemyStatus
{
    public int HP;
    public float Speed;
    public int AttackDamage;

    public EnemyStatus(int hP, float speed, int attackDamage)
    {
        HP = hP;
        Speed = speed;
        AttackDamage = attackDamage;
    }
}

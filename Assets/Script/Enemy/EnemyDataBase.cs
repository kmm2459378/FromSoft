using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBase : MonoBehaviour
{
    public static Dictionary<string, EnemyStatus> enemyStatusDic;

    void Awake()
    {
        enemyStatusDic = new Dictionary<string, EnemyStatus>();

        enemyStatusDic.Add("weak", new EnemyStatus(100, 2f, 10));
        enemyStatusDic.Add("Boss", new EnemyStatus(150, 3f, 15));
        enemyStatusDic.Add("BigBoss", new EnemyStatus(1000, 1.5f, 50));
    }
}

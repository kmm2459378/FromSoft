using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBase : MonoBehaviour
{

    public static Dictionary<EnemyType, EnemyStatus> enemyStatusDic
       = new Dictionary<EnemyType, EnemyStatus>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        enemyStatusDic.Clear();

        enemyStatusDic.Add(EnemyType.Weak, new EnemyStatus(100, 2f, 10));
        enemyStatusDic.Add(EnemyType.Boss, new EnemyStatus(150, 3f, 15));
        enemyStatusDic.Add(EnemyType.BigBoss, new EnemyStatus(1000, 1.5f, 50));
    }
}

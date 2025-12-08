using UnityEngine;

public interface EnemyState
{
    void Enter(Enemy enemy);
    void Update();
    void Exit();
}

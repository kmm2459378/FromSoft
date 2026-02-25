using UnityEngine;

public interface EnemyState
{
    EnemyStateType stateType { get;  }
    void Enter(Enemy enemy);
    void Update();
    void Exit();
}

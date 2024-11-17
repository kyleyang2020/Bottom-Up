using UnityEngine;

// abstract class for the different enemy states to copy/derive from when in those states
// i.e when player was in range or not in range
public abstract class BaseState_EnemyAI
{
    // player transform
    public Transform player;

    public abstract void EnterState(StateManager_EnemyAI enemy);
    // plays dectect player anim

    public abstract void UpdateState(StateManager_EnemyAI enemy);
    // start walking towards detected sound

    // public abstract void ExitState(EnemyStateManager enemy);
}
using UnityEngine;

/// <summary>
/// Targets state machine
/// </summary>
public class SMTarget: MonoBehaviour
{
    private TargetState CurrentState { get; set; }
    
    public void Init(TargetState _startState)
    {
        CurrentState = _startState;
        CurrentState.OnEnter();
    }
    
    public void ChangeState(TargetState _newState)
    {
        CurrentState = _newState;
        CurrentState.OnEnter();
    }
    
    private void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
    }
}
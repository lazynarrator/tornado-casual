using UnityEngine;

/// <summary>
/// State of idle
/// </summary>
public class IdleState : TargetState
{
    private Destruction component;
    
    public IdleState(ref Destruction _component)
    {
        component = _component;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
    }
}
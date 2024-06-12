using Scellecs.Morpeh;

/// <summary>
/// Disappearance/respawn state
/// </summary>
public class CollapseState : TargetState
{
    private Destruction component;
    private IdleState idleState;
    
    public CollapseState(ref Destruction _component)
    {
        component = _component;
    }
    
    public override void OnEnter()
    {
        World.Default.GetEvent<DestructionEvent>().NextFrame(new DestructionEvent() { EntityId = component.entityId });
        idleState ??= new IdleState(ref component);
        component.sm.ChangeState(idleState);
        base.OnEnter();
    }
}
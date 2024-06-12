/// <summary>
/// States for a target
/// </summary>
public abstract class TargetState
{
    public virtual void OnEnter() { }
    public virtual void OnFixedUpdate() { }
}
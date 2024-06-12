using UnityEngine;

/// <summary>
/// State of being attracted to a player
/// </summary>
public class PullState : TargetState
{
    private Destruction component;
    private RotateState rotateState;
    private readonly Transform target;
    
    public PullState(ref Destruction _component)
    {
        component = _component;
        target = component.objectPose;
    }
    
    public override void OnEnter()
    {
        if (component.playerPose == null)
            component.playerPose = GameObject.FindWithTag("Player").transform;
        target.SetParent(component.playerPose);
        
        base.OnEnter();
    }
    
    public override void OnFixedUpdate()
    {
        target.position = Vector3.Lerp(target.position, component.playerPose.position, component.pullSpeed * Time.fixedDeltaTime);
        
        if (Mathf.Abs(target.localPosition.x) < component.pullBorder && Mathf.Abs(target.localPosition.z) < component.pullBorder)
        {
            Vector3 directionToTarget = (component.playerPose.position - target.position).normalized;
            component.currentAngle = Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

            rotateState ??= new RotateState(ref component);
            component.sm.ChangeState(rotateState);
        }
        
        base.OnFixedUpdate();
    }
}
using UnityEngine;

/// <summary>
/// State of rotation around the player
/// </summary>
public class RotateState : TargetState
{
    private Destruction component;
    private CollapseState collapseState;
    private readonly Transform target;
    
    public RotateState(ref Destruction _component)
    {
        component = _component;
        target = component.objectPose;
    }
    
    public override void OnFixedUpdate()
    {
        float x = component.playerPose.position.x + component.radius * Mathf.Cos(Mathf.Deg2Rad * component.currentAngle);
        float z = component.playerPose.position.z + component.radius * Mathf.Sin(Mathf.Deg2Rad * component.currentAngle);
        Vector3 deltaPose = new Vector3(x, target.position.y + component.ySpeed * Time.fixedDeltaTime, z);
        target.position = Vector3.Lerp(target.position, deltaPose, Time.fixedDeltaTime);
        component.currentAngle -= component.rotationSpeed * Time.fixedDeltaTime;
        
        if (target.localScale.x > component.minScale)
        {
            target.localScale -= Vector3.one * (component.changeScaleSpeed * Time.fixedDeltaTime);
            target.localScale = new Vector3(
                Mathf.Max(target.localScale.x, component.minScale),
                Mathf.Max(target.localScale.y, component.minScale),
                Mathf.Max(target.localScale.z, component.minScale));
        }
        else
        {
            collapseState ??= new CollapseState(ref component);
            component.sm.ChangeState(collapseState);
        }
        
        base.OnFixedUpdate();
    }
}
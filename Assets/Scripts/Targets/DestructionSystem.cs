using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestructionSystem))]
public sealed class DestructionSystem : FixedUpdateSystem
{
    private Filter destructionFilter;
    private Event<CollideEvent> onCollided;
    
    public override void OnAwake()
    {
        destructionFilter = World.Filter.With<Destruction>().Build();
        foreach (var entity in destructionFilter)
        {
            ref var destructionComponent = ref entity.GetComponent<Destruction>();
            
            destructionComponent.entityId = entity.ID;
            destructionComponent.sm.Init(new IdleState(ref destructionComponent));
        }

        onCollided = World.GetEvent<CollideEvent>();
        onCollided.Subscribe(Collision);
    }
    
    private void Collision(FastList<CollideEvent> _collideEvents)
    {
        foreach (var collideEvent in _collideEvents)
        {
            foreach (var entity in destructionFilter)
            {
                if (collideEvent.EntityId == entity.ID)
                {
                    ref var destructionComponent = ref entity.GetComponent<Destruction>();
                    destructionComponent.sm.ChangeState(new PullState(ref destructionComponent));
                }
            }
        }
    }
    
    public override void OnUpdate(float deltaTime)
    {
        
    }
}
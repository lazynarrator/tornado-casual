using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CollideProvider : EntityProvider
{
    private World world;
    
    protected override void OnEnable()
    {
        world = World.Default;
        base.OnEnable();
    }
    
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            var instanceId = gameObject.GetInstanceID();
            if (map.TryGetValue(instanceId, out var item))
            {
                var entity = item.entity;
                world.GetEvent<CollideEvent>().NextFrame(new CollideEvent() { EntityId = entity.ID });
            }
        }
    }
}
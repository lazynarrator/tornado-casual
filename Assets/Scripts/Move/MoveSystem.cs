using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MoveSystem))]
public sealed class MoveSystem : UpdateSystem
{
    private Filter moveFilter;
    
    public override void OnAwake()
    {
        moveFilter = World.Filter.With<Movable>().With<MoveDirection>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in moveFilter)
        {
            ref var directionComponent = ref entity.GetComponent<MoveDirection>();
            ref var movableComponent = ref entity.GetComponent<Movable>();

            movableComponent.transform.position += 
                directionComponent.direction * (deltaTime * movableComponent.moveSpeed);
        }
    }
}
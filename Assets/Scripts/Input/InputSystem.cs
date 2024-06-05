using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputSystem))]
public sealed class InputSystem : UpdateSystem
{
    private Filter inputFilter;
    
    public override void OnAwake()
    {
        inputFilter = World.Filter.With<MoveDirection>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        foreach (var entity in inputFilter)
        {
            ref var directionComponent = ref entity.GetComponent<MoveDirection>();
            directionComponent.direction = new Vector3(x, directionComponent.direction.y, z);
        }
    }
}
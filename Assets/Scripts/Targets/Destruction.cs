using Scellecs.Morpeh;
using TriInspector;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Destruction : IComponent
{
    [ReadOnly] public EntityId entityId;
    public Transform objectPose;
    public SMTarget sm;
    
    [HideInInspector] public Transform playerPose;
    [HideInInspector] public float currentAngle;
    
    [Title("Pull")]
    public float pullSpeed;
    public float pullBorder;
    
    [Title("Rotate")]
    public float radius;
    public float rotationSpeed;
    public float ySpeed;
    public float changeScaleSpeed;
    public float minScale;
}
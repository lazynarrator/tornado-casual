using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(SpawnSystem))]
public sealed class SpawnSystem : Initializer
{
    [SerializeField] private SpawnProvider spawnedObject;
    [SerializeField] private int numObjectsToSpawn = 15;
    [SerializeField] private float borderOffsetX = 1f;
    [SerializeField] private float borderOffsetZ = 1f;
    [SerializeField] private float startYPose = 1f;
    
    MeshCollider planeCollider = new MeshCollider();
    
    private Filter spawnFilter;
    private Filter sceneFilter;
    
    private Event<DestructionEvent> onDestruction;
    
    private float[] posesX;
    private float[] posesZ;
    
    public override void OnAwake()
    {
        sceneFilter = World.Filter.With<SceneObject>().Build();
        Spawn();
        
        spawnFilter = World.Filter.With<Spawnable>().Build();
        onDestruction = World.GetEvent<DestructionEvent>();
        onDestruction.Subscribe(Respawn);
    }

    private void Spawn()
    {
        foreach (var sceneObject in sceneFilter)
        {
            planeCollider = sceneObject.GetComponent<SceneObject>().sceneObject.GetComponent<MeshCollider>();
        }
        
        Bounds planeBounds = planeCollider.bounds;
        float minX = planeBounds.min.x + borderOffsetX;
        float maxX = planeBounds.max.x - borderOffsetX;
        float minZ = planeBounds.min.z + borderOffsetZ;
        float maxZ = planeBounds.max.z - borderOffsetZ;
        
        posesX = new [] { minX, maxX };
        posesZ = new [] { minZ, maxZ };
        
        NewSpawn(numObjectsToSpawn);
    }
    
    private void NewSpawn(int _amount)
    {
        Vector3 planeScale = planeCollider.transform.localScale;
        
        for (int i = 0; i < _amount; i++)
        {
            SpawnProvider newObject = Instantiate(spawnedObject, GetSpawnPosition(), Quaternion.identity, planeCollider.transform);
            newObject.transform.localScale = new Vector3(Vector3.one.x/planeScale.x,
                Vector3.one.y/planeScale.y, Vector3.one.z/planeScale.z);
        }
    }
    
    private Vector3 GetSpawnPosition()
    {
        float randomX = Random.Range(posesX[0], posesX[1]);
        float randomZ = Random.Range(posesZ[0], posesZ[1]);
        Vector3 spawnPosition = new Vector3(randomX, startYPose, randomZ);
        return spawnPosition;
    }
    
    private void Respawn(FastList<DestructionEvent> _events)
    {
        foreach (var destructionEvent in _events)
        {
            foreach (var entity in spawnFilter)
            {
                if (destructionEvent.EntityId == entity.ID)
                {
                    ref var spawnComponent = ref entity.GetComponent<Spawnable>();
                    spawnComponent.objectPose.SetParent(planeCollider.transform);
                    
                    Vector3 planeScale = planeCollider.transform.localScale;
                    spawnComponent.objectPose.localScale = new Vector3(Vector3.one.x/planeScale.x,
                        Vector3.one.y/planeScale.y, Vector3.one.z/planeScale.z);
                    spawnComponent.objectPose.position = GetSpawnPosition();
                }
            }
        }
    }
}
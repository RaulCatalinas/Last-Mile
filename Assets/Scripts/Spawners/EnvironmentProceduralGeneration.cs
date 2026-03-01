using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProceduralGeneration : MonoBehaviour
{
    [Header("First Layer - Buildings")]
    [SerializeField] private List<GameObject> buildingChunks;
    [SerializeField] private Transform buildingRightSpawnPoint;
    [SerializeField] private Transform buildingLeftSpawnPoint;

    [Header("Second Layer - Ground")]
    [SerializeField] private List<GameObject> groundChunks;
    [SerializeField] private Transform groundRightSpawnPoint;
    [SerializeField] private Transform groundLeftSpawnPoint;

    [Header("Settings")]
    [SerializeField] private int initialChunks = 5;
    [SerializeField] private float buildingChunkWidth = 10f;
    [SerializeField] private float groundChunkWidth = 10f;

    [Header("Dynamic Parallax")]
    [SerializeField] private DynamicParallaxManager dynamicParallaxManager;

    [Header("Building Parallax Spawn Points")]
    [SerializeField] private Transform buildingParallaxRightSpawnPoint;
    [SerializeField] private Transform buildingParallaxLeftSpawnPoint;
    [SerializeField] private Transform buildingParallaxRightSpawnPointOutOfCamera;
    [SerializeField] private Transform buildingParallaxLeftSpawnPointOutOfCamera;


    private float buildingNextRightX;
    private float buildingNextLeftX;
    private float groundNextRightX;
    private float groundNextLeftX;

    void Awake()
    {
        buildingNextRightX = 0;
        buildingNextLeftX = 0;
    }

    void Start()
    {
        if (groundChunks != null && groundChunks.Count > 0) SpawnGroundChunk();

        for (int i = 0; i < initialChunks; i++)
        {
            SpawnBuildingChunk(
                buildingParallaxRightSpawnPoint,
                buildingParallaxLeftSpawnPoint
            );
        }

        buildingNextRightX = 0;
        buildingNextLeftX = 0;

        for (int i = 0; i < initialChunks; i++)
        {
            SpawnBuildingChunk(
                buildingParallaxRightSpawnPointOutOfCamera,
                buildingParallaxLeftSpawnPointOutOfCamera
            );
        }
    }

    void SpawnBuildingChunk(Transform rightSpawn, Transform leftSpawn)
    {
        var rightChunk = buildingChunks[Random.Range(0, buildingChunks.Count)];
        var leftChunk = buildingChunks[Random.Range(0, buildingChunks.Count)];

        var spawnRightX = rightSpawn.position.x + buildingNextRightX;
        var spawnLeftX = leftSpawn.position.x + buildingNextLeftX;

        var right = Instantiate(
            rightChunk,
            new Vector3(spawnRightX, rightSpawn.position.y, 0),
            Quaternion.identity,
            buildingRightSpawnPoint
        );
        var left = Instantiate(
            leftChunk,
            new Vector3(spawnLeftX, leftSpawn.position.y, 0),
            Quaternion.Euler(0, 0, 180),
            buildingLeftSpawnPoint
        );

        dynamicParallaxManager.RegisterObject(right, 0, buildingChunks);
        dynamicParallaxManager.RegisterObject(left, 1, buildingChunks);

        buildingNextRightX += buildingChunkWidth;
        buildingNextLeftX += buildingChunkWidth;
    }

    void SpawnGroundChunk()
    {
        var rightChunk = groundChunks[Random.Range(0, groundChunks.Count)];
        var leftChunk = groundChunks[Random.Range(0, groundChunks.Count)];

        Instantiate(
            rightChunk,
            new Vector3(
                groundNextRightX,
                groundRightSpawnPoint.position.y,
                0
            ),
            Quaternion.identity,
            groundRightSpawnPoint
        );
        Instantiate(
            leftChunk,
            new Vector3(
                groundNextLeftX,
                groundLeftSpawnPoint.position.y,
                0
            ),
            Quaternion.identity,
            groundLeftSpawnPoint
        );

        groundNextRightX += groundChunkWidth;
        groundNextLeftX -= groundChunkWidth;
    }
}

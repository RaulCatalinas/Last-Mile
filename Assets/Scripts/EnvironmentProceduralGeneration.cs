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

    private float buildingNextRightX;
    private float buildingNextLeftX;
    private float groundNextRightX;
    private float groundNextLeftX;

    void Start()
    {
        buildingNextRightX = 0;
        buildingNextLeftX = 0;
        groundNextRightX = groundRightSpawnPoint.position.x;
        groundNextLeftX = groundLeftSpawnPoint.position.x;

        SpawnGroundChunk();

        for (int i = 0; i < initialChunks; i++)
        {
            SpawnBuildingChunk();
        }
    }

    void SpawnBuildingChunk()
    {
        var rightChunk = buildingChunks[Random.Range(0, buildingChunks.Count)];
        var leftChunk = buildingChunks[Random.Range(0, buildingChunks.Count)];

        var spawnRightX = buildingParallaxRightSpawnPoint.position.x + buildingNextRightX;
        var spawnLeftX = buildingParallaxLeftSpawnPoint.position.x + buildingNextLeftX;

        var right = Instantiate(
            rightChunk,
            new Vector3(spawnRightX, buildingParallaxRightSpawnPoint.position.y, 0),
            Quaternion.identity,
            buildingRightSpawnPoint
        );
        var left = Instantiate(
            leftChunk,
            new Vector3(spawnLeftX, buildingParallaxLeftSpawnPoint.position.y, 0),
            Quaternion.Euler(0, 0, 180),
            buildingLeftSpawnPoint
        );

        dynamicParallaxManager.RegisterObject(right, 0);
        dynamicParallaxManager.RegisterObject(left, 0);

        buildingNextRightX += buildingChunkWidth;
        buildingNextLeftX += buildingChunkWidth;
    }

    void SpawnGroundChunk()
    {
        var rightChunk = groundChunks[Random.Range(0, groundChunks.Count)];
        var leftChunk = groundChunks[Random.Range(0, groundChunks.Count)];

        Instantiate(
            rightChunk,
            new Vector3(groundNextRightX, groundRightSpawnPoint.position.y, 0),
            Quaternion.identity,
            groundRightSpawnPoint
        );
        Instantiate(
            leftChunk,
            new Vector3(groundNextLeftX,
            groundLeftSpawnPoint.position.y, 0),
            Quaternion.identity,
            groundLeftSpawnPoint
        );

        groundNextRightX += groundChunkWidth;
        groundNextLeftX -= groundChunkWidth;
    }
}

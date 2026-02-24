using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProceduralGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> environmentChunks;
    [SerializeField] private int initialChunks = 5;
    [SerializeField] private float chunkWidth = 10f;
    [SerializeField] private Transform startRightPoint;
    [SerializeField] private Transform startLeftPoint;

    private float nextSpawnRightX;
    private float nextSpawnLeftX;

    void Start()
    {
        nextSpawnRightX = startRightPoint.position.x;
        nextSpawnLeftX = startLeftPoint.position.x;

        for (int i = 0; i < initialChunks; i++)
        {
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        var chunk = environmentChunks[Random.Range(0, environmentChunks.Count)];

        Instantiate(
            chunk,
            new Vector3(nextSpawnRightX, startRightPoint.position.y, 0),
            Quaternion.identity,
            startRightPoint
        );
        nextSpawnRightX += chunkWidth;

        Instantiate(
            chunk,
            new Vector3(nextSpawnLeftX, startLeftPoint.position.y, 0),
            new Quaternion(
                Quaternion.identity.x,
                Quaternion.identity.y,
                180,
                Quaternion.identity.w
            ),
            startLeftPoint
        );
        nextSpawnLeftX -= chunkWidth;
    }
}
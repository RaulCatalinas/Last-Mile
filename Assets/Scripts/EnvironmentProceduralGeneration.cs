using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProceduralGeneration : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField] private List<GameObject> firstLayerChunks;
    [SerializeField] private List<GameObject> secondLayerChunks;
    [SerializeField] private int initialChunks = 5;
    [SerializeField] private float chunkWidth = 10f;

    [Header("Spawn Points")]
    [SerializeField] private Transform firstLayerStartRightPoint;
    [SerializeField] private Transform firstLayerStartLeftPoint;

    [Header("Background Spawn Points")]
    [SerializeField] private Transform secondLayerStartRightPoint;
    [SerializeField] private Transform secondLayerStartLeftPoint;

    private float firstLayerNextRightX;
    private float firstLayerNextLeftX;

    void Start()
    {
        firstLayerNextRightX = firstLayerStartRightPoint.position.x;
        firstLayerNextLeftX = firstLayerStartLeftPoint.position.x;

        SpawnSecondLayerChunk();

        for (int i = 0; i < initialChunks; i++)
        {
            SpawnFirstLayerChunk();
        }

    }

    void SpawnFirstLayerChunk()
    {
        var chunk = firstLayerChunks[Random.Range(0, firstLayerChunks.Count)];

        Instantiate(
            chunk,
            new Vector3(
                firstLayerNextRightX,
                firstLayerStartRightPoint.position.y,
                0
            ),
            Quaternion.identity,
            firstLayerStartRightPoint
        );
        firstLayerNextRightX += chunkWidth;

        Instantiate(
            chunk,
            new Vector3(
                firstLayerNextLeftX,
                firstLayerStartLeftPoint.position.y,
                0
            ),
            new Quaternion(
                Quaternion.identity.x,
                Quaternion.identity.y,
                180,
                Quaternion.identity.w
            ),
            firstLayerStartLeftPoint
        );
        firstLayerNextLeftX -= chunkWidth;
    }

    void SpawnSecondLayerChunk()
    {
        var chunkRight = secondLayerChunks[Random.Range(0, secondLayerChunks.Count)];
        var chunkLeft = secondLayerChunks[Random.Range(0, secondLayerChunks.Count)];

        Instantiate(
            chunkRight,
            secondLayerStartRightPoint.position,
            Quaternion.identity,
            secondLayerStartRightPoint
        );


        Instantiate(
            chunkLeft,
            secondLayerStartLeftPoint.position,
            Quaternion.identity,
            secondLayerStartLeftPoint
        );
    }
}
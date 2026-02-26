using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProceduralGeneration : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField] private List<GameObject> firstLayerChunks;
    [SerializeField] private List<GameObject> secondLayerChunks;
    [SerializeField] private int initialChunks = 5;
    [SerializeField] private float chunkWidth = 10f;

    [Header("Spawn")]
    [SerializeField, Range(1f, 5f)] private float spawnInterval = 2f;

    [Header("Spawn Points")]
    [SerializeField] private Transform firstLayerStartRightPoint;
    [SerializeField] private Transform firstLayerStartLeftPoint;

    [Header("Background Spawn Points")]
    [SerializeField] private Transform secondLayerStartRightPoint;
    [SerializeField] private Transform secondLayerStartLeftPoint;

    [Header("Parallax")]
    [SerializeField] private Transform firstLayerStartRightPointParallax;
    [SerializeField] private Transform firstLayerStartLeftPointParallax;

    private float firstLayerNextRightX;
    private float firstLayerNextLeftX;
    private float parallaxNextRightX;
    private float parallaxNextLeftX;

    void Awake()
    {
        firstLayerNextRightX = firstLayerStartRightPoint.position.x;
        firstLayerNextLeftX = firstLayerStartLeftPoint.position.x;

        parallaxNextRightX = firstLayerStartRightPointParallax.position.x;
        parallaxNextLeftX = firstLayerStartLeftPointParallax.position.x;
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        SpawnSecondLayerChunk();

        for (int i = 0; i < initialChunks; i++)
        {
            SpawnFirstLayerChunk(
                ref firstLayerNextRightX,
                ref firstLayerNextLeftX,
                firstLayerStartRightPoint,
                firstLayerStartLeftPoint
            );
        }

    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            for (int i = 0; i < initialChunks; i++)
            {
                SpawnFirstLayerChunk(
                    ref parallaxNextRightX,
                    ref parallaxNextLeftX,
                    firstLayerStartRightPointParallax,
                    firstLayerStartLeftPointParallax
                );
            }
        }
    }

    void SpawnFirstLayerChunk(
        ref float nextRightX,
        ref float nextLeftX,
        Transform rightParent,
        Transform leftParent
    )
    {
        var rightChunk = firstLayerChunks[Random.Range(0, firstLayerChunks.Count)];
        var leftChunk = firstLayerChunks[Random.Range(0, firstLayerChunks.Count)];

        Instantiate(
            rightChunk,
            new Vector3(nextRightX, rightParent.position.y, 0),
            Quaternion.identity,
            rightParent
        );

        Instantiate(
            leftChunk,
            new Vector3(nextLeftX, leftParent.position.y, 0),
            Quaternion.Euler(0, 0, 180),
            leftParent
        );

        nextRightX += chunkWidth;
        nextLeftX -= chunkWidth;
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
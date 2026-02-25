using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private List<Sprite> carsSprites;
    [SerializeField] private List<Sprite> motorcycleSprites;

    [Header("Spawn")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField, Range(1f, 5f)] private float spawnInterval = 5f;
    [SerializeField] private float minimumSpawnInterval = 0.5f;


    [Header("Prefabs")]
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject motorcyclePrefab;

    [Header("Score manager")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("Traffic")]
    [SerializeField, Range(3f, 15f)] private float minimumSpeed = 5f;
    [SerializeField, Range(3f, 15f)] private float maximumSpeed = 8f;
    [SerializeField] private float topSpeed = 30f;

    public static ObstacleSpawner Instance { get; private set; }

    const int WAVES = 10;
    int spawnCounter = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        spawnCounter = 0;
        StartCoroutine(SpawnRoutine());
        SpawnObstacle();
    }

    public IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObstacle();

            spawnCounter++;

            if (spawnCounter == WAVES)
            {
                IncreaseDifficulty();
                spawnCounter = 0;
            }
        }
    }

    public void SpawnObstacle()
    {
        var spawnCar = Random.Range(0, 2) == 0;
        var obstacle = InstantiatePrefab(spawnCar ? carPrefab : motorcyclePrefab);

        if (spawnCar) obstacle.GetComponent<SpriteRenderer>().sprite = GetRandomCarSprite();
        else obstacle.GetComponent<SpriteRenderer>().sprite = GetRandomMotorcycleSprite();
    }

    private Sprite GetRandomCarSprite()
    {
        return carsSprites[Random.Range(0, carsSprites.Count)];
    }

    private Sprite GetRandomMotorcycleSprite()
    {
        return motorcycleSprites[Random.Range(0, motorcycleSprites.Count)];
    }

    private GameObject InstantiatePrefab(GameObject prefab)
    {
        var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        return Instantiate(
            prefab,
            new Vector3(randomSpawnPoint.position.x, randomSpawnPoint.position.y, prefab.transform.position.z),
            prefab.transform.rotation
        );
    }

    public float GetRandomSpeed()
    {
        return Random.Range(minimumSpeed, maximumSpeed);
    }

    void IncreaseDifficulty()
    {
        if (spawnInterval < minimumSpawnInterval)
        {
            spawnInterval *= 0.9f;
        }

        if (maximumSpeed < topSpeed)
        {
            minimumSpeed *= 1.1f;
            maximumSpeed *= 1.1f;
        }
    }
}
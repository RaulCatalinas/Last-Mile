using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    public GameObject prefab;
    public List<Sprite> sprites;
    public bool isAmbulance;
}

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private List<ObstacleData> obstacles;

    [Header("Spawn")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField, Range(1f, 5f)] private float spawnInterval = 5f;
    [SerializeField] private float minimumSpawnInterval = 0.5f;

    [Header("Score manager")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("Traffic")]
    [SerializeField, Range(3f, 15f)] private float minimumSpeed = 5f;
    [SerializeField, Range(3f, 15f)] private float maximumSpeed = 8f;
    [SerializeField] private float topSpeed = 30f;
    [SerializeField, Range(0f, 1f)] private float ambulanceProbability = 0.2f;
    [SerializeField] private float ambulanceIncreaseRate = 0.05f;

    public static ObstacleSpawner Instance { get; private set; }

    const int WAVES = 10;
    int spawnCounter = 0;
    ObstacleData selectedObstacle;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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

            if (spawnCounter >= WAVES)
            {
                IncreaseDifficulty();
                spawnCounter = 0;
            }
        }
    }

    public void SpawnObstacle()
    {
        if (obstacles == null || obstacles.Count == 0) return;

        if (Random.value < ambulanceProbability)
        {
            selectedObstacle = obstacles.Find(obstacle => obstacle.isAmbulance);

            if (selectedObstacle == null) selectedObstacle = obstacles[Random.Range(0, obstacles.Count)];
        }
        else
        {
            var normalObstacles = obstacles.FindAll(obstacle => !obstacle.isAmbulance);

            if (normalObstacles.Count > 0) selectedObstacle = normalObstacles[Random.Range(0, normalObstacles.Count)];
            else selectedObstacle = obstacles[Random.Range(0, obstacles.Count)];
        }

        var obstacle = InstantiatePrefab(selectedObstacle.prefab);

        if (selectedObstacle.sprites != null && selectedObstacle.sprites.Count > 0)
        {
            var sprite = selectedObstacle.sprites[Random.Range(0, selectedObstacle.sprites.Count)];
            obstacle.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    private GameObject InstantiatePrefab(GameObject prefab)
    {
        var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        return Instantiate(
            prefab,
            new Vector3(
                randomSpawnPoint.position.x,
                randomSpawnPoint.position.y,
                prefab.transform.position.z
            ),
            prefab.transform.rotation
        );
    }

    public float GetRandomSpeed()
    {
        return Random.Range(minimumSpeed, maximumSpeed);
    }

    void IncreaseDifficulty()
    {
        if (spawnInterval > minimumSpawnInterval)
        {
            spawnInterval *= 0.9f;
        }

        if (maximumSpeed < topSpeed)
        {
            minimumSpeed *= 1.1f;
            maximumSpeed *= 1.1f;
        }

        if (ambulanceProbability < 1f)
        {
            ambulanceProbability += ambulanceIncreaseRate;
            ambulanceProbability = Mathf.Clamp01(ambulanceProbability);
        }
    }
}

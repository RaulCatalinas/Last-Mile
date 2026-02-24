using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Sprite> carsSprites;
    [SerializeField] private List<Sprite> motorcycleSprites;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject motorcyclePrefab;
    [SerializeField, Range(1f, 5f)] private float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    public IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObstacle();
        }
    }

    public void SpawnObstacle()
    {
        var spawnCar = Random.Range(0, 2) == 0;
        var obstacle = InstantiatePrefab(spawnCar ? carPrefab : motorcyclePrefab);


        if (spawnCar)
        {
            obstacle.GetComponent<SpriteRenderer>().sprite = GetRandomCarSprite();
        }
        else
        {
            obstacle.GetComponent<SpriteRenderer>().sprite = GetRandomMotorcycleSprite();
        }
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
}
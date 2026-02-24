using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Sprite> carsSprites;
    [SerializeField] private List<Sprite> motorcycleSprites;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject motorcyclePrefab;
    [SerializeField, Range(1f, 5f)] private float spawnInterval = 2f;

    private List<float> spawnYCoordinates = new List<float> {
        0.42f,
        -1.07f,
        0f,
        -2.125777f,
        1.435776f
    };

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
        var spawnYCoordinate = spawnYCoordinates[Random.Range(0, spawnYCoordinates.Count)];
        var spawnX = Camera.main.ViewportToWorldPoint(new Vector3(1.2f, 0, 0)).x;

        return Instantiate(
            prefab,
            new Vector3(spawnX, spawnYCoordinate, prefab.transform.position.z),
            prefab.transform.rotation
        );
    }
}
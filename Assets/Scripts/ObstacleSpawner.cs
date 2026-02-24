using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Sprite> carsSprites;
    [SerializeField] private List<Sprite> motorcycleSprites;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject motorcyclePrefab;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnObstacle()
    {
        bool spawnCar = Random.Range(0, 2) == 0;

        if (spawnCar)
        {
            GameObject car = Instantiate(carPrefab, transform.position, Quaternion.identity);
            car.GetComponent<SpriteRenderer>().sprite = GetRandomCarSprite();
        }
        else
        {
            GameObject motorcycle = Instantiate(motorcyclePrefab, transform.position, Quaternion.identity);
            motorcycle.GetComponent<SpriteRenderer>().sprite = GetRandomMotorcycleSprite();
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
}
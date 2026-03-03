using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [SerializeField] private List<PowerUpData> powerUps;
    [SerializeField] private List<PowerUpData> trollPowerUps;
    [SerializeField] private GameObject powerUpPrefab;

    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Spawn Settings")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField, Range(0f, 1f)] private float trollProbability = 0.3f;
    [SerializeField] private float minDistanceFromPlayer = 1f;

    void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    PowerUpData GetRandomPowerUpData()
    {
        var powerUpList = Random.value < trollProbability ? trollPowerUps : powerUps;
        return powerUpList[Random.Range(0, powerUpList.Count)];
    }

    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Transform spawnPoint;

            do
            {
                var randomIndex = Random.Range(0, spawnPoints.Count);
                spawnPoint = spawnPoints[randomIndex];
            } while (
                Vector2.Distance(spawnPoint.position, player.position) < minDistanceFromPlayer
            );

            var powerUpData = GetRandomPowerUpData();
            var powerUpInstance = Instantiate(
                powerUpPrefab,
                spawnPoint.position,
                Quaternion.identity
            );
            var powerUpController = powerUpInstance.GetComponent<PowerUpController>();
            powerUpController.Initialize(powerUpData);
        }
    }
}

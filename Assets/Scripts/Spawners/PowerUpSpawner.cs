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

    [SerializeField] private HashSet<Transform> occupiedSpawnPoints = new HashSet<Transform>();
    [SerializeField] private List<Transform> availableSpawnPoints = new List<Transform>();

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

            availableSpawnPoints.Clear();

            foreach (var point in spawnPoints)
            {
                bool isOccupied = occupiedSpawnPoints.Contains(point);
                bool tooCloseToPlayer = Vector2.Distance(point.position, player.position) < minDistanceFromPlayer;

                if (!isOccupied && !tooCloseToPlayer) availableSpawnPoints.Add(point);
            }

            if (availableSpawnPoints.Count == 0)
            {
                Debug.LogWarning("No spawn points available.");
                continue;
            }

            var spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];

            var powerUpInstance = Instantiate(
                powerUpPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

            occupiedSpawnPoints.Add(spawnPoint);

            var powerUpData = GetRandomPowerUpData();
            var powerUpController = powerUpInstance.GetComponent<PowerUpController>();

            powerUpController.Initialize(powerUpData, spawnPoint, this);
        }
    }

    public void FreeSpawnPoint(Transform point)
    {
        occupiedSpawnPoints.Remove(point);
    }
}

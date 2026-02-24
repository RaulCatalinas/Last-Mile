using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProceduralGeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> environmentItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateEnvironment()
    {
        var environmentItem = environmentItems[Random.Range(0, environmentItems.Count)];
    }
}

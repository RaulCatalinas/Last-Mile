using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DynamicParallaxLayer
{
    [HideInInspector] public List<GameObject> spawnedObjects = new List<GameObject>();
    [HideInInspector] public List<GameObject> possibleChunks = new List<GameObject>();
    public float speed;
    public float chunkWidth;
    [Range(0f, 1f)] public float gapProbability = 0.2f;
    public int maxGapChunks = 3;

    [HideInInspector] public List<GameObject> toRemove = new List<GameObject>();
    [HideInInspector] public List<GameObject> toAdd = new List<GameObject>();
}

public class DynamicParallaxManager : MonoBehaviour
{
    [SerializeField] private List<DynamicParallaxLayer> layers;

    private float leftLimit;

    void Awake()
    {
        leftLimit = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        foreach (var layer in layers)
        {
            MoveLayer(layer);
        }
    }

    void MoveLayer(DynamicParallaxLayer layer)
    {
        layer.toRemove.Clear();
        layer.toAdd.Clear();

        foreach (var obj in layer.spawnedObjects)
        {
            obj.transform.position += Vector3.left * layer.speed * Time.deltaTime;

            if (obj.transform.position.x + layer.chunkWidth / 2f < leftLimit)
            {
                var rightmostX = GetRightmostX(layer.spawnedObjects);
                var newChunk = layer.possibleChunks[Random.Range(0, layer.possibleChunks.Count)];

                GameObject spawned;

                if (Random.value < layer.gapProbability)
                {
                    var gapSize = Random.Range(1, layer.maxGapChunks + 1);

                    spawned = Instantiate(
                        newChunk,
                        new Vector3(
                            rightmostX + layer.chunkWidth * (gapSize + 1),
                            obj.transform.position.y,
                            obj.transform.position.z
                        ),
                        obj.transform.rotation,
                        obj.transform.parent
                    );
                }
                else
                {
                    spawned = Instantiate(
                        newChunk,
                        new Vector3(
                            rightmostX + layer.chunkWidth,
                            obj.transform.position.y,
                            obj.transform.position.z
                        ),
                        obj.transform.rotation,
                        obj.transform.parent
                    );
                }

                layer.toAdd.Add(spawned);
                layer.toRemove.Add(obj);
            }
        }

        foreach (var obj in layer.toRemove)
        {
            layer.spawnedObjects.Remove(obj);
            Destroy(obj);
        }

        foreach (var obj in layer.toAdd)
        {
            layer.spawnedObjects.Add(obj);
        }
    }

    float GetRightmostX(List<GameObject> objects)
    {
        var rightmostX = float.MinValue;

        foreach (var obj in objects)
        {
            if (obj.transform.position.x > rightmostX)
                rightmostX = obj.transform.position.x;
        }

        return rightmostX;
    }

    public void RegisterObject(GameObject obj, int layerIndex, List<GameObject> possibleChunks)
    {
        if (layerIndex >= 0 && layerIndex < layers.Count)
        {
            layers[layerIndex].spawnedObjects.Add(obj);

            if (layers[layerIndex].possibleChunks.Count == 0)
            {
                layers[layerIndex].possibleChunks = possibleChunks;
            }
        }
    }
}

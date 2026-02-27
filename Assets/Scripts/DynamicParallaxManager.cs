using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DynamicParallaxLayer
{
    public List<GameObject> spawnedObjects = new List<GameObject>();
    public float speed;
    public float chunkWidth;
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
        foreach (var obj in layer.spawnedObjects)
        {
            obj.transform.position += Vector3.left * layer.speed * Time.deltaTime;

            if (obj.transform.position.x + layer.chunkWidth / 2f < leftLimit)
            {
                var rightmostX = GetRightmostX(layer.spawnedObjects);

                obj.transform.position = new Vector3(
                    rightmostX + layer.chunkWidth,
                    obj.transform.position.y,
                    obj.transform.position.z
                );
            }
        }
    }

    float GetRightmostX(List<GameObject> objects)
    {
        var rightmostX = float.MinValue;

        foreach (var obj in objects)
        {
            if (obj.transform.position.x > rightmostX)
            {
                rightmostX = obj.transform.position.x;
            }
        }

        return rightmostX;
    }

    public void RegisterObject(GameObject obj, int layerIndex)
    {
        if (layerIndex >= 0 && layerIndex < layers.Count)
        {
            layers[layerIndex].spawnedObjects.Add(obj);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParallaxLayer
{
    public List<GameObject> objects;
    public float speed;
    public float objectWidth;
}

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private List<ParallaxLayer> layers;

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

    void MoveLayer(ParallaxLayer layer)
    {
        foreach (var obj in layer.objects)
        {
            obj.transform.position += Vector3.left * layer.speed * Time.deltaTime;

            if (obj.transform.position.x + layer.objectWidth / 2f < leftLimit)
            {
                var rightmostX = GetRightmostX(layer.objects);

                obj.transform.position = new Vector3(
                    rightmostX + layer.objectWidth,
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
            if (obj.transform.position.x > rightmostX) rightmostX = obj.transform.position.x;
        }

        return rightmostX;
    }
}

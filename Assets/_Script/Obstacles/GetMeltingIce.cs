using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeltingIce : MonoBehaviour {
    [SerializeField] private Transform ObstaclePoint;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float spawnRate;

    void Update()
    {
        if (transform.position.x < ObstaclePoint.position.x)
        {
            generateObstacble();
        }
    }

    void generateObstacble()
    {
        float YRange = Random.Range(0, 1.5f);
        int i = Random.Range(0, 3);
        this.GetComponent<SpriteRenderer>().sprite = sprites[i];
        transform.position = new Vector3(transform.position.x + spawnRate, 3f + YRange,
                transform.position.z);
    }
}

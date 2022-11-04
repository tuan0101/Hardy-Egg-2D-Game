using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    [SerializeField] private Transform ObstaclePoint;
    [SerializeField] private ObjectPooler[] theObjectPool;
    private float coLumnMin;
	private float coLumnMax;

	// Update is called once per frame
	void Update () {
        if (transform.position.x < ObstaclePoint.position.x)
        {
            generateObstacble();
        }
    }

    void generateObstacble()
    {
            float spawnYPos = Random.Range(coLumnMin, coLumnMax);
            float spawnRate = Random.Range(5f, 8f);
            int selector = Random.Range(0, theObjectPool.Length);
            if (selector == 1)
            {
                transform.position = new Vector3(transform.position.x + spawnRate + 7f, spawnYPos,
                transform.position.z);
            }
            else
            transform.position = new Vector3(transform.position.x + spawnRate, spawnYPos,
                transform.position.z);
        
            GameObject newObstacle = theObjectPool[selector].getPooledObject();
            newObstacle.transform.position = transform.position;

        if (selector == 1)
        {
            transform.position = new Vector3(transform.position.x + 7f, spawnYPos,
            transform.position.z);
        }
        newObstacle.transform.rotation = transform.rotation;
    }
}
